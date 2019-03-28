
--DECLARE @LandingPageId varchar ='81624623-f021-407f-8ae2-f1a0a15e9dd3'

SELECT * FROM  [landing].[LandingPages]
SELECT * FROM [landing].[InfoRequests]
SELECT * FROM [landing].FormHds
SELECT * FROM [dbo].[FormHdLandingPage] WHERE FormHdId  = '30e8b6c3-3e11-41a3-8042-374e630824e2'
--delete FROM [landing].[LandingPages] WHERE [Id] NOT IN( '81624623-f021-407f-8ae2-f1a0a15e9dd3')
delete FROM [dbo].[FormHdLandingPage] WHERE [LandingPageId] NOT IN( '81624623-f021-407f-8ae2-f1a0a15e9dd3')


--DELETE FROM [landing].[FormDetails] 
--DELETE FROM [landing].[FormHds]
--DELETE FROM [landing].[DDLCatalogs]
--DELETE FROM [landing].[InfoRequests]
--DELETE FROM [dbo].[FormHdLandingPage]
--DELETE FROM [landing].[LandingPages]