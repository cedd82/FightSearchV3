
-- see how often fighters are watched by users, each watch count represents a link click on the video

DECLARE @fightertable TABLE
(
  watchcount int, 
  fightername nvarchar(500)
)

insert into @fightertable
select sum(watchcount), fighter1name 
from wikifight
where watchcount is not null
group by fighter1name
order by 1 desc


insert into @fightertable
select sum(watchcount), fighter2name 
from wikifight
where watchcount is not null
group by fighter2name
order by 1 desc

select * from @fightertable
order by 1 desc