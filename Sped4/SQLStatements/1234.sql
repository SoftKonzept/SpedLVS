USE [SLE_LVS]
DECLARE @iCount Integer;
DECLARE @iRows Integer;
DECLARE @EAID varchar(20);
DECLARE @AbBereich decimal;
DECLARE @Mandant decimal;

SET @AbBereich = 3;
SET @Mandant= 1;

SET @iCount=1;
SET @iRows = (Select MAX(ID) FROM [SLE_LVS].dbo.LEingang);

WHILE(@iCount<=@iRows)
BEGIN
	IF ((Select AbBereich FROM [SLE_LVS].dbo.LEingang WHERE ID=@iCount)=@AbBereich) 
	BEGIN
	    SET @EAID='';
		SET @EAID=(Select EAIDaltLVS FROM [SLE_LVS].dbo.LEingang WHERE ID=@iCount);
		IF (@EAID<>'')
		BEGIN
			Update [SLE_LVS].dbo.Artikel  
				SET LEingangTableID=@iCount 
				, AB_ID=@AbBereich
				, Mandanten_ID= @Mandant
				WHERE EAEingangAltLVS=@EAID;
		END
	END
	SET @iCount=@iCount+1; 
END


SELECT a.[ID]
      , a.[LVS_ID]
      , a.[LEingangTableID]
      , a.[LAusgangTableID]
  FROM [SLE_LVS].[dbo].[Artikel] a
  INNER JOIN [SLE_LVS].[dbo].LEingang b on b.ID=a.LEingangTableID
  WHERE a.LEingangTableID >0 AND b.AbBereich=@AbBereich and b.Mandant=@Mandant

 SELECT 
     COUNT(DISTINCT a.[LEingangTableID])
  FROM [SLE_LVS].[dbo].[Artikel] a
  INNER JOIN [SLE_LVS].[dbo].LEingang b on b.ID=a.LEingangTableID
  WHERE a.LEingangTableID >0 AND b.AbBereich=@AbBereich and b.Mandant=@Mandant
