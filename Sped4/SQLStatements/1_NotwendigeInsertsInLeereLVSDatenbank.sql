USE [Test_LVS]
GO
/*** 1. *********** Güterart Alle Güter hinterlegen ************/
INSERT INTO Gueterart
           ([ViewID]
           ,[Bezeichnung]
           ,[Dicke]
           ,[Breite]
           ,[Laenge]
           ,[Hoehe]
           ,[MassAnzahl]
           ,[Netto]
           ,[Brutto]
           ,[ArtikelArt]
           ,[Besonderheit]
           ,[Verpackung]
           ,[AbsteckBolzenNr]
           ,[MEAbsteckBolzen]
           ,[Arbeitsbereich]
           ,[LieferantenID]
           ,[aktiv]
		   ,[Mindestbestand]
		   ,[BestellNr]
		   ,[Zusatz]
		   ,[Einheit]
           ,[Verweis])
     VALUES(0, 'Alle Güter', 0.0, 0.0 ,0.0 , 0.0 , 4, 0.0 , 0.0, '', '', '','' , '', 0, 0 , 1 , 0 
           , '' , '' , '', '' )	
GO
/*** 2. ************ ADR -> Client ***********************/
INSERT INTO ADR
           ([ViewID]
           ,[KD_ID]
           ,[Fbez]
           ,[Name1]
           ,[Name2]
           ,[Name3]
           ,[Str]
           ,[HausNr]
           ,[PF]
           ,[PLZ]
           ,[PLZPF]
           ,[ORT]
           ,[OrtPF]
           ,[Land]
           ,[Wavon]
           ,[Wabis]
           ,[Dummy]
           ,[tmpStr]
           ,[tmpHausNr]
           ,[LKZ]
           ,[UserInfoTxt]
           ,[activ]
           ,[Lagernummer]
           ,[ASNCom]
           ,[AdrID_Be]
           ,[AdrID_Ent]
           ,[AdrID_Post]
           ,[AdrID_RG]
           ,[IsAuftraggeber]
           ,[IsVersender]
           ,[IsBelade]
           ,[IsEmpfaenger]
           ,[IsEntlade]
           ,[IsPost]
           ,[IsRG]
           ,[CalcLagerVers]
           ,[DocEinlagerAnzeige]
           ,[DocAuslagerAnzeige]
           ,[Verweis]
           ,[PostRGBy]
           ,[PostAnlageBy]
           ,[PostLfsBy]
           ,[PostListBy]
           ,[IsDiv]
           ,[IsSpedition]
           ,[PostAnzeigeBy])
     VALUES
('SLE'
, 0 
, 'Firma' 
, 'SLE Stahl-Logistik Eisenhüttenstadt'
, 'GmbH & Co. KG'	
, ''
, 'Straße 6'	
, '2'
, ''
, '15890'
, '' 
, 'Eisenhüttenstadt'
, 'Eisenhüttenstadt'
, 'Deutschland'
, CAST('01.01.1900 07:00' as DateTime)
, CAST('01.01.1900 17:00' as DateTime)
, 0 
, '' 
, ''
, 'D'
, ''
, 1
, 0
, 0
, 0
, 0
, 0
, 0
, 1
, 1
, 1 --as IsBelade
, 1 --as IsEmpfaenger
, 1 --as IsEntlade
, 1 --as IsPost
, 1 --as IsRG
, 0 --as CalcLagerVer
, '' --as DocEinlagerAnzeige
, '' --as DocAuslagerAnzeige
, '' --as Verweis
, 0 --as PostRGBy
, 0 --as PostAnlageBy
, 0 --as PostLfsBy
, 0 --as PostListBy
, 0 --as IsDiv
, 0 --as IsSped
, 0 --as PostAnzeigeBy
)
GO
/******* Primekeys für den 1. Arbeitsbereich anlegen ****/
INSERT INTO PrimeKeys
           ([Mandanten_ID]
           ,[AuftragsNr]
           ,[LfsNr]
           ,[LvsNr]
           ,[RGNr]
           ,[GSNr]
           ,[LEingangID]
           ,[LAusgangID])
     VALUES
           (1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1)
GO
/***   User ADMIN anlegen *************/
INSERT INTO [User]
           ([Name]
           ,[LoginName]
           ,[Pass]
           ,[Initialen]
           ,[Vorname]
           ,[Tel]
           ,[Fax]
           ,[Mail]
           ,[dtDispoVon]
           ,[dtDispoBis]
           ,[FontSize]
           ,[SMTPUser]
           ,[SMTPPass]
           ,[SMTPServer]
           ,[SMTPPort])
     VALUES
           ('Administrator'
            ,'Admin'
            ,'lvs'
            ,'admin'
            ,''
            ,''
            ,''
            ,''
            ,Null
            ,Null
            ,8.5
            ,''
            ,''
            ,''
            ,0)
            
INSERT INTO Userberechtigungen
           ([UserID]
           ,[read_ADR]
           ,[write_ADR]
           ,[read_Kunde]
           ,[write_Kunde]
           ,[read_Personal]
           ,[write_Personal]
           ,[read_KFZ]
           ,[write_KFZ]
           ,[read_Gut]
           ,[write_Gut]
           ,[read_Relation]
           ,[write_Relation]
           ,[read_Order]
           ,[write_Order]
           ,[write_TransportOrder]
           ,[read_TransportOrder]
           ,[write_Disposition]
           ,[read_Disposition]
           ,[read_FaktLager]
           ,[write_FaktLager]
           ,[read_FaktSpedition]
           ,[write_FaktSpedition]
           ,[read_Bestand]
           ,[read_LagerEingang]
           ,[write_LagerEingang]
           ,[read_LagerAusgang]
           ,[write_LagerAusgang]
           ,[read_User]
           ,[write_User]
           ,[read_Arbeitsbereich]
           ,[write_Arbeitsbereich]
           ,[read_Mandant]
           ,[write_Mandant]
           ,[read_Statistik]
           ,[read_Einheit]
           ,[write_Einheit]
           ,[read_Schaden]
           ,[write_Schaden]
           ,[read_LagerOrt]
           ,[write_LagerOrt]
           ,[read_ASNTransfer]
           ,[write_ASNTransfer]
           ,[read_FaktExtraCharge]
           ,[write_FaktExtraCharge])
     VALUES
           (1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
            ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1
           ,1)
GO

