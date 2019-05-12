FightSearch is a .net core and angular 7 web application. It serves as a video aggregator for mma bouts for UFC, bellator, pride, strikeforce and WEC. It can be visited at https://www.mmavideosearch.com

Fights can be filtered on a variety of data not available on https://www.ufc.tv and http://www.bellator.com/videos. This is acheived by obtaining data from other sources and matching them to the videos. This is not done as part of this project but another one

The EF query only hits one table as it's constructed when updating the data which is only done every couple of months, so it allows faster performance than joining over 3 tables. Not a huge performance improvement but it was a pragmatic solution for what i needed.

I strive to keep this up to date with the latest technologies. Originally it was written in angularjs and asp.net web api. It is now in angular 7 and .net core 2.1
