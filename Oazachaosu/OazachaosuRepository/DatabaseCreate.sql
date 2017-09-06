﻿DECLARE @CurrentMigration [nvarchar](max)

IF object_id('[dbo].[__MigrationHistory]') IS NOT NULL
    SELECT @CurrentMigration =
        (SELECT TOP (1) 
        [Project1].[MigrationId] AS [MigrationId]
        FROM ( SELECT 
        [Extent1].[MigrationId] AS [MigrationId]
        FROM [dbo].[__MigrationHistory] AS [Extent1]
        WHERE [Extent1].[ContextKey] = N'OazachaosuRepository.Migrations.Configuration'
        )  AS [Project1]
        ORDER BY [Project1].[MigrationId] DESC)

IF @CurrentMigration IS NULL
    SET @CurrentMigration = '0'

IF @CurrentMigration < '201704210751129_Start'
BEGIN
    CREATE TABLE [dbo].[ArticleCategory] (
        [Id] [int] NOT NULL IDENTITY,
        [ArticleId] [int] NOT NULL,
        [CategoryId] [smallint] NOT NULL,
        CONSTRAINT [PK_dbo.ArticleCategory] PRIMARY KEY ([Id])
    )
    CREATE TABLE [dbo].[ArticleComment] (
        [Id] [int] NOT NULL IDENTITY,
        [ArticleId] [int] NOT NULL,
        [UserName] [nvarchar](32),
        [Content] [nvarchar](512),
        [DateTime] [datetime] NOT NULL,
        CONSTRAINT [PK_dbo.ArticleComment] PRIMARY KEY ([Id])
    )
    CREATE TABLE [dbo].[Article] (
        [Id] [int] NOT NULL IDENTITY,
        [Title] [nvarchar](64),
        [Abstract] [nvarchar](512),
        [ContentUrl] [nvarchar](64),
        [DateTime] [datetime] NOT NULL,
        CONSTRAINT [PK_dbo.Article] PRIMARY KEY ([Id])
    )
    CREATE TABLE [dbo].[Category] (
        [Id] [smallint] NOT NULL IDENTITY,
        [Name] [nvarchar](64),
        [Url] [nvarchar](128),
        CONSTRAINT [PK_dbo.Category] PRIMARY KEY ([Id])
    )
    CREATE TABLE [dbo].[Group] (
        [Id] [bigint] NOT NULL IDENTITY,
        [UserId] [bigint] NOT NULL,
        [Name] [nvarchar](64),
        [Language1] [tinyint] NOT NULL,
        [Language2] [tinyint] NOT NULL,
        [State] [tinyint] NOT NULL,
        [LastUpdateTime] [datetime],
        CONSTRAINT [PK_dbo.Group] PRIMARY KEY ([Id])
    )
    CREATE TABLE [dbo].[Result] (
        [Id] [bigint] NOT NULL IDENTITY,
        [UserId] [bigint] NOT NULL,
        [GroupId] [bigint] NOT NULL,
        [Correct] [smallint] NOT NULL,
        [Accepted] [smallint] NOT NULL,
        [Wrong] [smallint] NOT NULL,
        [Invisibilities] [smallint] NOT NULL,
        [TimeCount] [smallint] NOT NULL,
        [TranslationDirection] [tinyint] NOT NULL,
        [LessonType] [tinyint] NOT NULL,
        [DateTime] [datetime] NOT NULL,
        [State] [tinyint] NOT NULL,
        [LastUpdateTime] [datetime],
        CONSTRAINT [PK_dbo.Result] PRIMARY KEY ([Id])
    )
    CREATE TABLE [dbo].[AspNetRoles] (
        [Id] [nvarchar](128) NOT NULL,
        [Name] [nvarchar](256) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY ([Id])
    )
    CREATE UNIQUE INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]([Name])
    CREATE TABLE [dbo].[AspNetUserRoles] (
        [UserId] [nvarchar](128) NOT NULL,
        [RoleId] [nvarchar](128) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId])
    )
    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]([UserId])
    CREATE INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]([RoleId])
    CREATE TABLE [dbo].[AspNetUsers] (
        [Id] [nvarchar](128) NOT NULL,
        [LocalId] [bigint] NOT NULL,
        [Name] [nvarchar](32),
        [Password] [nvarchar](32),
        [ApiKey] [nvarchar](32),
        [IsAdmin] [bit] NOT NULL,
        [CreateDateTime] [datetime] NOT NULL,
        [LastLoginDateTime] [datetime] NOT NULL,
        [State] [tinyint] NOT NULL,
        [Email] [nvarchar](256),
        [EmailConfirmed] [bit] NOT NULL,
        [PasswordHash] [nvarchar](max),
        [SecurityStamp] [nvarchar](max),
        [PhoneNumber] [nvarchar](max),
        [PhoneNumberConfirmed] [bit] NOT NULL,
        [TwoFactorEnabled] [bit] NOT NULL,
        [LockoutEndDateUtc] [datetime],
        [LockoutEnabled] [bit] NOT NULL,
        [AccessFailedCount] [int] NOT NULL,
        [UserName] [nvarchar](256) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY ([Id])
    )
    CREATE UNIQUE INDEX [UserNameIndex] ON [dbo].[AspNetUsers]([UserName])
    CREATE TABLE [dbo].[AspNetUserClaims] (
        [Id] [int] NOT NULL IDENTITY,
        [UserId] [nvarchar](128) NOT NULL,
        [ClaimType] [nvarchar](max),
        [ClaimValue] [nvarchar](max),
        CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY ([Id])
    )
    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]([UserId])
    CREATE TABLE [dbo].[AspNetUserLogins] (
        [LoginProvider] [nvarchar](128) NOT NULL,
        [ProviderKey] [nvarchar](128) NOT NULL,
        [UserId] [nvarchar](128) NOT NULL,
        CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId])
    )
    CREATE INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]([UserId])
    CREATE TABLE [dbo].[Word] (
        [Id] [bigint] NOT NULL IDENTITY,
        [UserId] [bigint] NOT NULL,
        [GroupId] [bigint] NOT NULL,
        [Language1] [nvarchar](128),
        [Language2] [nvarchar](128),
        [Drawer] [tinyint] NOT NULL,
        [Language1Comment] [nvarchar](128),
        [Language2Comment] [nvarchar](128),
        [Visible] [bit] NOT NULL,
        [State] [tinyint] NOT NULL,
        [LastUpdateTime] [datetime],
        CONSTRAINT [PK_dbo.Word] PRIMARY KEY ([Id])
    )
    ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id])
    ALTER TABLE [dbo].[AspNetUserRoles] ADD CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
    ALTER TABLE [dbo].[AspNetUserClaims] ADD CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
    ALTER TABLE [dbo].[AspNetUserLogins] ADD CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
    CREATE TABLE [dbo].[__MigrationHistory] (
        [MigrationId] [nvarchar](150) NOT NULL,
        [ContextKey] [nvarchar](300) NOT NULL,
        [Model] [varbinary](max) NOT NULL,
        [ProductVersion] [nvarchar](32) NOT NULL,
        CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
    )
    INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
    VALUES (N'201704210751129_Start', N'OazachaosuRepository.Migrations.Configuration',  0x1F8B0800000000000400ED5DDB72DBB819BEEF4CDF81A3ABB6E3B57C48DCD463EF8E23DBA9A671E2B1EC6CEF329004CB9C90A09607C7DE4E9FAC177DA4BE420112240110000912A4E45D8F6F2C1C3E1CFE0F3F7E1CF8E37FFFF9EFC94F4FBEE73CC2307203743ADADFDD1B39102D82A58B56A7A324BEFFE1DDE8A71FFFF887938BA5FFE47CC9D31D927438278A4E470F71BC3E1E8FA3C503F441B4EBBB8B308882FB787711F863B00CC6077B7B7F1BEFEF8F218618612CC739B94950ECFA30FD817F4E02B480EB3801DE55B0845E44C371CC2C45753E011F466BB080A7A3CFE057B078004194DCC07510B971103EEFA6D946CE99E7025CA319F4EE470E402888418CEB7B7C17C1591C0668355BE300E0DD3EAF214E770FBC08D2761C97C99B3669EF8034695C66CCA116491407BE21E0FE21EDA3B198BD554F8F8A3EC4BD78817B3B7E26AD4E7BF2747416C6EEC2831310C315EEBF912396793CF142925ED7DFBB02CA8E234BBB53B006938BFCED3893C48B93109E2298C421F0769CEB64EEB98B7FC0E7DBE01B44A728F13CB6FEB805388E0BC041D761B08661FC7C03EF69ABA6CB9133E6F38DC58C4536264FD6D0298A0F0F46CE275C38987BB0A007D32933DC20F8012218E2262FAF411CC310110C987670A574A12CDA5FF545EA61F2FEE670F68F6A704EC6250B1A7123F07DDCAC8ED4C8405E99310C33B09E0BC97F390A567A588F8F9C2BF0F411A255FC80353746BD749FE0320FA0C077C8C55A1FE789C3A49E81018A536E288B79BB6FA39C73DC99B76ED99EF2B755AA77E3F82BB96BA478EBC69E8E92476F2C50E56C1EE14E5DF4CF49CAFDBBD0EBBB497DB3BFE3ECFFDB99F6A573A73DFED768642B5CD1D371FFE09D71198D69F4210C92754B0EA5795F3E8188087B241099D6EBCBDB3C093F02B44AC00AEE1705E185D86E1E6A5CE53CE3811DB8191606E4A0688871B5A2F86EBD542AE75683E8064698DC2D475196F975180D308C5285D515641284212C4D9526AB3789C1B3205B26D0680D5845F999EC8A748398A2473772E7AEE7C62E8CBA619111340912D4B16F6E4380222F25CDB94BBA3ADD456146BE3C81A9228051142082CA2B2826D810B1A5C5F7C2745D3E5E6F02FDD2EBAAD8DF3A8BD69F60BC9B67DCCD202F430CF73D08BFEDB2883B4EE37CA56E3C68AA1B0FF7E7F787EFDE1E81E5E1D11B78F876783DD9C1BCB36B321CBC3DB252EA27F0E8AE52D14BF435D62737301BA8D183BBA66466E5FD9526BB0C039FFCE6F995C57E9D0549B8208D0994496E41B88271474A1328FBB4CE51B79FDAF9143BAE4F4A1AD46624F0B3F870A321AF6FBFE536661CE98696E622C9FAC28DC5E1C4FE315800AFE7C597953DD96B104578E4EBBAC84A39676B17CBA9EF52A6D1D9D2770BA3ED7D80471F3037D12621C4B68A35B38A98431F83958BB6CE50BBF081ABDB016A3A5937286512A07B37F4CB95475BE1E48CFD3B881E3455C7FF5AA8FA0C2E9210AB50DCBDFEBAF7D2AE1F02043F25FE9C28E9E1CAB2269ADBEFC12558E029E102915C9DF1B016FD1624F1055A929172172FF44B090D8095EA90B573145D6232C3A5B8DCECE7D8AD776379E201D7975BCBA47A5FF3F8D24C66822BF6311B27338C75354915A4A62679BC50932C585E131A675A1382A0A9088D16EA9186CAAB9145595B28A4FD6B7FA590C26EFF5261DB4F2D37B5CE48C5C76D2AF53571A4257D015E62BBA856A3211DE3F647430ABBFDA321AD260E7E7497C46468B07ECE13A7C67883F4F2A579FD98136A36F470E09A3974E1C3E880C6C3E5E77479D76ACD4FB2BEF035FFEFE880A87A906AE74CBFC9196B5F259D87E07BA940DE3FB73A8DA0DD52DC4D1CAA77862AF00B39492B2F4BB55DDF6CD7F14FE2333AAC3CBE9F46971E5895D7B0D57A4DD466D12E018D8AAB003B4E777586E5846719EF19CB956128DF3B57902CB2736581BEA1E03B964D6A419D8EF62A9DC925BF402BCF25FB1C34F9BE3EF975C0A53ED0A7FE0043DC71CF45F2437DF2CB10A24509FE469F7AB60688ADCBDBBA9A8731160A5E29C122CB913ECB4D12452E28BBF2AFFAE4D318786CF27755226694D3D0908E87EE1C4C813646C0EF4163029E07083665DF14E199306ECABE4C433425DF39F42093FA4D0BE1C94FEFBBC85286B811A192CD884B378CE2A6622519667011A0A54AB88DBA94BDB460413117682F413563500C9DD5B9E908B974A35FBFB9429E9A713225178723F7110AD96A06CCE4217017629E1A9D7D03D032F0853C358AFB3D8C6221C75113269DE12C0B376586E482073D9EE7CBBD404BA7FEACBEB45EF81B23579834EE1AD30487C804A545CF77EF24E8E5E13D5FC25F2A25E0250D0C4926404E43C8C57317C5D5F58F8B16EE1A78B54D1572365C3B117914658831E7700D1129B0B6279A142E3FAB271528CA119675753D74326668A36713BB7DAD12B4742FBB94707658DE9C37B2ED6F0561E8266E2F8C91B46A00AA481ADFA454E5D58FC138420F16B442154F193A724438985070846E6DF6C711BE554371846FFCCBE04876E6A395A97000D49121FC99D1F0B34EB54943D1836BF996B123DBAA209F72E11C850594DEF5399FA75F783DC92EE3E31A52DB37A2FB39A2EC09EC0CC6F93933F781747A3BBADC24A13CA87C8A5DE1931C33DB91D221E67B56CD003548B510DA06366E59BA752A43A05F18D564CFBE8290E5CF3FAEA80308E4BDC05B9F3520E2B0D6019643BF06945E71AD00651AC9A046F92D006D95A84963009B1FE96B61E92C58034B0E2A6448D9D987909919DDD58AB197839984EA2BC4A2DA69B44A29EA5FF0A7A2BE1AAD47181C0985C45313BEE10D3A85BB0352ED0DA58D5D6B6533F5A67DAD69BFCCAE56343CAFAB9D96E70455B45C6639D6DA8EE62D17AC4545CBF3BADA69392590A2E11273A8CE20326F366F0259227A7EDC50CCE145DCC938731A43034EC60AEF322757604D367E186F3334C49965AE66263FCCCC7DAFF819C678C1F5AB68711425C54108565088259F882D61BA0F780E623007641B6EB2F42BC9048B45A15AF3C2544649557EB9F2CD7392FFB3DCCDDDC1486C3E0A77899B4C4C93B4F59021442D82433C02010F849253E249E0253E52DBAFEADC8CF70D1684096E8EC53A6761C1D8F02ADAC958E89A8A095C9189305044611B5121B7252D13811AA0ED79A002D87E1A94574259A832D48050B9AF158E4D7960739CF2D4960552DDBFCD48B54952DA66637B1A0EC53FEA278505A041061C2EDCA070142E428D79973A9690502F0DFF0DB2CFFEB4D8613E1C7A22AC6A2C536D55A18B82271B922EDD62B024D96C5FC25CAC8A7CFDC834DF5513E7A1A199C1DC696361986073AC033956E51C5687456F82B03834C8A43EFC3D29BE527CDCD68C857CBFCCD260A09B6CE6A34195719B874371CB938529024D2659EA09849F6169A0C1B45F3803E1A6FD22B439127508C2C2D0A0E618A247104E5A429C897954F806E14DA422D8004B7A83888395A6305009CC851A4E1D30E17D9A4FAFEA4E54778AADAE56CA8E3B12305779FAEC9BB6ED3624A0EA6EA0656115C72DED05A686E877BEC92FE0B028AA4B391B13A0EA14A595D0D2A32D7341C9B3F533A20AD7109CFECB0387B4AD4BDF0F2C4A196A604850EF0E9C1941C30C0C80DC7B03D7B979A0817924B86FE0AC2421CE6CFA12BC38883398103DECF44A7D39B01834C810837107500163E2CC7996796C90712D8B31E82FDE2D03D76F7C94412D59E70B5C25D98856788A1E95A73030472BEE163853B4126BA4A344C70B82B612A35B604BEA2CC6992D6504DF0CE29A4688EE73577E0B6C21E5B17C676328BBE8D1CD1A52606CF3E29DF9C29E9B50CA60432CFA0D7D058C866F25AB94571E3AB32ABBE7D38D550A0CB526E2BE51E71591F6C37A3526F7E139A7EC751FDEABF1CCB8BB218664B7AF2C9122BDB265CE0379B66D5628B67603B7758B3CFF749BDB7BA2612D5A575C4C953652716DB5515BB5C8072D908BEFB159C022F075D74D8814AF3715498AD26948F1BBB88C452F42D5BFFF55B91995251939B95EC65DFB1CC5D0DF25097667BF7813CF4DC59E27B802C8BD87519C7D62383AD823EF82704F876DCF335EE3285A72BE36EADFF2E2C53680630E97746EADEB0D53176DE28B4869299D5FCA8A7CE079F5506D1FCB7AED7BA5533CF408426C2884155FA1256E8BA7A7A4A8E9433F66B0A2434FA24AE31E5E9AFA6D10E4967DD1492A02DEE94D9B079C6C49B6FA5E93A50AF74D99CD29F45249DA264EAD423017439D6053C73C3A48C3878E0697C6DC5DF5220BDEDB555ECAE6055A717E15BBE8B94DDD2ABEADDA0271EE94DAD746E64B49A6343A50547697E677CF51C1255B4B14E1D19E8686A4649E13DEED690DC43DDDD31A45FE7A4F6BB8CA033EED91346FF8B41E8395177BDA22B59CFC5FB26E515F5A1942C33499DB3BCC58954FBDA768099F4E47FF4A731DA79FB391FFD2E01D671ADD21F7970447DCE20E75FE5DF5F56DC706D4DF3BD9D237599AF7EAF49F5FB3AC3BCEE77009C363674FE8CB3612E65F6A31AA4D96B5436DCCDE6F79B9A3487816A52FEBD17C7F407C06C5122CFFEA892550E19193B9DBC22A913E70D27A36523E70B2D9F98D7BDD44DAF982CE6DFF98491B21C81E32C96BF9271F3CFDD9B46AD2C74A3A214A1E24B18567A50B550F8EB4C1523E36D2CC20D20076AE9AF2E111BB3BAC75335D9E7383D68CE4DEC88BDD0EDD2EF3A7F28044A7815E7D24C200AEC343102D98F1C2DE50B0668B5D579F48B086BD496A9BBD8BF0BAE7657FCFABB215DB6E87BDC1D6AC2D60FE9581AE3BC7E24303D69BDF13BEF08C401B4B656BF7A5ACB91BB6EA61B874E526011DD0B1F090BE8435DF5335DD5A72B6CB857027779F12177A0A2EF4ED33786812A8EE911BEC456E9393606B2CA01E0F152CE8DB2BF0D02C50DDFBDE4616D4BB01B646824DCD0A9BA040E3D960E3AE7EABFEF54459D2CBAE8A4B960DFCF866F752B1B9330FB0FC3333A79BB3DF06BE7E75859AF903D6B903D694520BAFE9427DDF99F91156BB1196619B7818D63818964137743ECC1BA09512F86869FFA7EF5ACADD68AA0A2B5590B2C03289BA50B5FF4EB1E04C91560ACB82F50598B58A9A58DA66D134FA62157E687565D3895D5B364DA32F5BE109562C3B738D5C292E0B9695B035BE94A5DE60651EB16B6657098AD2B7F6067D27579CE7366E264767C53798961A6AC35572FB86726347F159A0A58676F78CDCBE9936696BE009B9FA910D36AC12443663B25FE730725725C409C644D9D5AB12344F3345F7416ED90935CA9308DB415730064B6C6F1153E11E2C621C4D8EC3D2A734E98B5317FE1C2EA7E87312AF93183719FA738F9BF18985A82B3F75F7CCD7F9E4F33A7DFEDD461370355DB27FF519BD4F5CAF7C73ED52B27DAD8020A627DD2E26B28CC9B6F1AA7CBBF153FA4E5F1320DA7D85C57C0BFDB587C1A2CF68061E619BBA61FA7D842BB0782E0F2B5420F582E0BBFDE4DC05AB10F811C528F3E39F98C34BFFE9C7FF03029E3AE891A20000 , N'6.1.3-40302')
END

