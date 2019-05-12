
-- 1 backup tables then drop them, this is done to preserve watch count and to copy the tables from local db where data updates are done
--drop table wikifight1
----drop table fight1
--drop table wikievent1
--drop table wikifightweb1

--select * into wikifight1 from wikifight
----select * into fight1 from fight
--select * into wikievent1 from wikievent
--select * into wikifightweb1 from wikifightweb

select watchcount, * from wikifight
order by 1 desc

----drop table fight
--drop table wikievent
--drop table wikifight


-- 2 copy tables across in export data in ssms

-- 3 update the watch count on the new imported table

begin tran
UPDATE wikifight
SET  wikifight.WatchCount = wf1.WatchCount
FROM wikifight wf
INNER JOIN wikifight1 wf1 ON wf1.id = wf.id
rollback

-- 4 update the table used to retrieve data by the api. faster than joining between fight, wikifight and wikievent...

truncate table wikifightweb

insert into wikifightweb (WikiFightId,WeightClass,Fighter1Name,Fighter2Name,FightResult,FightResultHow,FightResultType,FightResultSubType,Round,Time,TotalTime,totaltimeenum, EventId,FightId,ImageForWeb,RedditTopFights,FOTN,POTN,TitleFight,VideoLink,ImagePath,DateHeld,EventName,Promotion)
select wf.[Id],[WeightClass],[Fighter1Name],[Fighter2Name],[FightResult],[FightResultHow],[FightResultType],[FightResultSubType],[Round],[Time],[TotalTime],
-- totaltimeenum
CASE   
      WHEN --r1
		DATEDIFF(second, totaltime , '00:5:00') >= 0 and 
		DATEDIFF(second, totaltime , '00:00:00') <= 0 THEN 1	-- <= 5mins
      WHEN --r1-r2 but rnd 3 not started
		DATEDIFF(second, totaltime , '00:10:00') >= 0 and 
		DATEDIFF(second, totaltime , '00:05:00') <= 0 THEN 2
	  WHEN --r2 -r3 but not finished
		DATEDIFF(second, totaltime , '00:15:00') > 0 and 
		DATEDIFF(second, totaltime , '00:10:00') <= 0 and
		wf.FightResultType not like '%Decision%'
		 THEN 3
	  WHEN	-- r3 finished only
		DATEDIFF(second, totaltime , '00:15:00') >= 0 and
		DATEDIFF(second, totaltime , '00:10:00') <= 0 THEN 4
	  WHEN -- more than r3
		DATEDIFF(second, totaltime , '00:15:00') < 0 THEN 5
   END,
[EventId],[FightId],replace(imageforweb,'Assets/images/fightImages/',''),
-- reddit fights
CASE
	WHEN [RedditTopFights] IS NULL THEN 0
	ELSE [RedditTopFights]
END,
-- FOTN
CASE
	WHEN [FOTN] IS NULL THEN 0
	ELSE [FOTN]
END,
-- POTN
CASE
	WHEN [POTN] IS NULL THEN 0
	ELSE [POTN]
END,
-- [TitleFight]
CASE
	WHEN [TitleFight] IS NULL THEN 0
	ELSE [TitleFight]
END
,[VideoLink],replace(imagepath,'Assets\images\fightthumbs\',''), DateHeld, e.name ,Promotion
FROM [wikifight] wf
join wikievent e on wf.eventid = e.id  
and fightid is not null
order by e.DateHeld desc

-- rebuild all indexes
--ALTER INDEX ALL ON dbo.wikifightweb
--REORGANIZE ;   




