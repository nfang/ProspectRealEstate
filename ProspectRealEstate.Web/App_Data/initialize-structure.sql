CREATE TABLE [dbo].[Users] (
    [ID]            TINYINT        NOT NULL,
    [login]         NVARCHAR (60)  NOT NULL,
    [pass]          NVARCHAR (64)  NOT NULL,
    [display_name]  NVARCHAR (50)  NOT NULL,
    [email]         NVARCHAR (100) NOT NULL,
    [registered_on] DATETIME       NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Agents] (
    [ID]            TINYINT        IDENTITY (1, 1) NOT NULL,
    [name]          NVARCHAR (100) NOT NULL,
    [phone]         NVARCHAR (20)  NOT NULL,
    [fax]           NVARCHAR (20)  NULL,
    [email]         NVARCHAR (100) NOT NULL,
    [profile_photo] NVARCHAR (MAX) NULL,
    [added_on]      DATETIME       NOT NULL,
    [added_by]      TINYINT        NOT NULL,
    [modified_on]   DATETIME       NULL,
    [modified_by]   TINYINT        NULL,
    CONSTRAINT [PK_Agents] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Agents_Users_Added] FOREIGN KEY ([added_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_Agents_Users_Modified] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[Users] ([ID])
);

CREATE TABLE [dbo].[Languages] (
    [ID]           TINYINT       IDENTITY (1, 1) NOT NULL,
    [lang_name]    NVARCHAR (10) NOT NULL,
    [display_name] NVARCHAR (50) NOT NULL,
    [iso_639x]     NVARCHAR (10) NULL,
    CONSTRAINT [PK_Languages] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Countries] (
    [ID]             SMALLINT      IDENTITY (1, 1) NOT NULL,
    [country_code]   NVARCHAR (10) NOT NULL,
    [telephone_code] NVARCHAR (10) NOT NULL,
    [common_name]    NVARCHAR (64) NOT NULL,
    [formal_name]    NVARCHAR (64) NOT NULL,
    [lang]           TINYINT       NOT NULL,
    CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Countries_Languages] FOREIGN KEY ([lang]) REFERENCES [dbo].[Languages] ([ID])
);

CREATE TABLE [dbo].[Categories] (
    [ID]          TINYINT       IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (64) NOT NULL,
    [added_by]    TINYINT       NOT NULL,
    [added_on]    DATETIME      NOT NULL,
    [modified_by] TINYINT       NULL,
    [modified_on] DATETIME      NULL,
    [lang]        TINYINT       NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Categories_Users_Added] FOREIGN KEY ([added_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_Categories_Users_Modified] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_Categories_Languages] FOREIGN KEY ([lang]) REFERENCES [dbo].[Languages] ([ID])
);

CREATE TABLE [dbo].[States] (
    [ID]         TINYINT       IDENTITY (1, 1) NOT NULL,
    [state_name] NVARCHAR (50) NOT NULL,
    [lang]       TINYINT       NOT NULL,
    CONSTRAINT [PK_States] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_States_Languages] FOREIGN KEY ([lang]) REFERENCES [dbo].[Languages] ([ID])
);

CREATE TABLE [dbo].[Suburbs] (
    [ID]          SMALLINT      IDENTITY (1, 1) NOT NULL,
    [suburb_name] NVARCHAR (60) NOT NULL,
    [state]       TINYINT       NOT NULL,
    [lang]        TINYINT       NOT NULL,
    CONSTRAINT [PK_Suburbs] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Suburbs_Languages] FOREIGN KEY ([lang]) REFERENCES [dbo].[Languages] ([ID]),
    CONSTRAINT [FK_Suburbs_States] FOREIGN KEY ([state]) REFERENCES [dbo].[States] ([ID])
);

CREATE TABLE [dbo].[PropertyTypes] (
    [ID]          TINYINT       IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (64) NOT NULL,
    [added_by]    TINYINT       NOT NULL,
    [added_on]    DATETIME      NOT NULL,
    [modified_by] TINYINT       NULL,
    [modified_on] DATETIME      NULL,
    [lang]        TINYINT       NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PropertyTypes_Users_Added] FOREIGN KEY ([added_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_PropertyTypes_Users_Modified] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_PropertyTypes_Languages] FOREIGN KEY ([lang]) REFERENCES [dbo].[Languages] ([ID])
);

CREATE TABLE [dbo].[Pages] (
    [ID]          TINYINT        IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (50)  NOT NULL,
    [title]       NVARCHAR (MAX) NOT NULL,
    [content]     NVARCHAR (MAX) NOT NULL,
    [added_by]    TINYINT        NOT NULL,
    [added_on]    DATETIME       NOT NULL,
    [modified_by] TINYINT        NULL,
    [modified_on] DATETIME       NULL,
    [lang]        TINYINT        NULL,
    CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Pages_Languages] FOREIGN KEY ([lang]) REFERENCES [dbo].[Languages] ([ID]),
    CONSTRAINT [FK_Pages_Users_Added] FOREIGN KEY ([added_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_Pages_Users_Modified] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[Users] ([ID])
);

CREATE TABLE [dbo].[Properties] (
    [ID]              INT               IDENTITY (1, 1) NOT NULL,
    [property_code]   NVARCHAR (10)     NOT NULL,
    [property_type]   TINYINT           NOT NULL,
    [num_of_bedroom]  TINYINT           NOT NULL,
    [num_of_bathroom] TINYINT           NOT NULL,
    [num_of_carspace] TINYINT           NOT NULL,
    [land_area]       INT               NULL,
    [description]     NVARCHAR (MAX)    NULL,
    [rent_or_sale]    NVARCHAR (10)     NULL,
    [featured]        BIT               NOT NULL,
    [street]          NVARCHAR (100)    NOT NULL,
    [suburb]          SMALLINT          NOT NULL,
    [state]           TINYINT           NOT NULL,
    [country]         SMALLINT          NOT NULL,
    [postcode]        NVARCHAR (10)     NOT NULL,
    [min_price]       INT               NULL,
    [max_price]       INT               NULL,
    [latlng]          [sys].[geography] NULL,
    [agent]           TINYINT           NULL,
    [added_on]        DATETIME          NOT NULL,
    [added_by]        TINYINT           NOT NULL,
    [modified_on]     DATETIME          NULL,
    [modified_by]     TINYINT           NULL,
    [status]          NVARCHAR (20)     NULL,
    [lang]            TINYINT           NOT NULL,
    CONSTRAINT [PK_Properties] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Properties_Agents] FOREIGN KEY ([agent]) REFERENCES [dbo].[Agents] ([ID]),
    CONSTRAINT [FK_Properties_Countries] FOREIGN KEY ([country]) REFERENCES [dbo].[Countries] ([ID]),
    CONSTRAINT [FK_Properties_PropertyTypes] FOREIGN KEY ([property_type]) REFERENCES [dbo].[PropertyTypes] ([ID]),
    CONSTRAINT [FK_Properties_States] FOREIGN KEY ([state]) REFERENCES [dbo].[States] ([ID]),
    CONSTRAINT [FK_Properties_Users_Added] FOREIGN KEY ([added_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_Properties_Users_Modified] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_Properties_Languages] FOREIGN KEY ([lang]) REFERENCES [dbo].[Languages] ([ID]),
    CONSTRAINT [FK_Properties_Suburbs] FOREIGN KEY ([suburb]) REFERENCES [dbo].[Suburbs] ([ID])
);

CREATE TABLE [dbo].[Businesses] (
    [ID]            INT               IDENTITY (1, 1) NOT NULL,
    [business_code] NVARCHAR (10)     NOT NULL,
    [category]      TINYINT           NOT NULL,
    [title]         NVARCHAR (100)    NOT NULL,
    [description]   NVARCHAR (MAX)    NULL,
    [street]        NVARCHAR (100)    NOT NULL,
    [suburb]        SMALLINT          NOT NULL,
    [state]         TINYINT           NOT NULL,
    [country]       SMALLINT          NOT NULL,
    [postcode]      NVARCHAR (10)     NOT NULL,
    [featured]      BIT               NOT NULL,
    [latlng]        [sys].[geography] NULL,
    [asking]        BIGINT            NOT NULL,
    [taking]        BIGINT            NULL,
    [rent]          BIGINT            NULL,
    [lease]         NVARCHAR (20)     NULL,
    [agent]         TINYINT           NULL,
    [added_on]      DATETIME          NOT NULL,
    [added_by]      TINYINT           NOT NULL,
    [modified_on]   DATETIME          NULL,
    [modified_by]   TINYINT           NULL,
    [status]        NVARCHAR (20)     NULL,
    [lang]          TINYINT           NOT NULL,
    CONSTRAINT [PK_Businesses] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Businesses_Categories] FOREIGN KEY ([category]) REFERENCES [dbo].[Categories] ([ID]),
    CONSTRAINT [FK_Businesses_Users_Added] FOREIGN KEY ([added_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_Businesses_Users_Modified] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_Businesses_Agents] FOREIGN KEY ([agent]) REFERENCES [dbo].[Agents] ([ID]),
    CONSTRAINT [FK_Businesses_Countries] FOREIGN KEY ([country]) REFERENCES [dbo].[Countries] ([ID]),
    CONSTRAINT [FK_Businesses_States] FOREIGN KEY ([state]) REFERENCES [dbo].[States] ([ID]),
    CONSTRAINT [FK_Businesses_Languages] FOREIGN KEY ([lang]) REFERENCES [dbo].[Languages] ([ID]),
    CONSTRAINT [FK_Businesses_Suburbs] FOREIGN KEY ([suburb]) REFERENCES [dbo].[Suburbs] ([ID])
);

CREATE TABLE [dbo].[Attachments] (
    [type]          NVARCHAR (10)  NOT NULL,
    [property_id]   INT            NULL,
    [business_id]   INT            NULL,
    [file_location] NVARCHAR (MAX) NULL,
    [added_on]      DATETIME       NOT NULL,
    [added_by]      TINYINT        NOT NULL,
    [modified_on]   DATETIME       NULL,
    [modified_by]   TINYINT        NULL,
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Attachments] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Attachments_Properties] FOREIGN KEY ([property_id]) REFERENCES [dbo].[Properties] ([ID]),
    CONSTRAINT [FK_Attachments_Users_Added] FOREIGN KEY ([added_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_Attachments_Users_Modified] FOREIGN KEY ([modified_by]) REFERENCES [dbo].[Users] ([ID]),
    CONSTRAINT [FK_Attachments_Businesses] FOREIGN KEY ([business_id]) REFERENCES [dbo].[Businesses] ([ID])
);
