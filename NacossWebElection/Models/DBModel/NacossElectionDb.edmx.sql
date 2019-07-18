
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/18/2019 06:56:05
-- Generated from EDMX file: D:\ProJects\NacossWebElection\NacossWebElection\Models\DBModel\NacossElectionDb.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [nacossvotingdb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Candidates_Positions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Candidates] DROP CONSTRAINT [FK_Candidates_Positions];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidateVote_Candidates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CandidateVote] DROP CONSTRAINT [FK_CandidateVote_Candidates];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidateVote_CandidateVoteCount]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CandidateVote] DROP CONSTRAINT [FK_CandidateVote_CandidateVoteCount];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidateVote_Positions]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CandidateVote] DROP CONSTRAINT [FK_CandidateVote_Positions];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidateVote_Voters]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CandidateVote] DROP CONSTRAINT [FK_CandidateVote_Voters];
GO
IF OBJECT_ID(N'[dbo].[FK_CandidateVoteCount_Candidates]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CandidateVoteCount] DROP CONSTRAINT [FK_CandidateVoteCount_Candidates];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_Logins_dbo_Users_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Logins] DROP CONSTRAINT [FK_dbo_Logins_dbo_Users_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_UserClaims_dbo_Users_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserClaims] DROP CONSTRAINT [FK_dbo_UserClaims_dbo_Users_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_UserRoles_dbo_Roles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_dbo_UserRoles_dbo_Roles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_UserRoles_dbo_Users_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_dbo_UserRoles_dbo_Users_UserId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Candidates]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Candidates];
GO
IF OBJECT_ID(N'[dbo].[CandidateVote]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CandidateVote];
GO
IF OBJECT_ID(N'[dbo].[CandidateVoteCount]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CandidateVoteCount];
GO
IF OBJECT_ID(N'[dbo].[Logins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logins];
GO
IF OBJECT_ID(N'[dbo].[Positions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Positions];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[UserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserClaims];
GO
IF OBJECT_ID(N'[dbo].[UserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Voters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Voters];
GO
IF OBJECT_ID(N'[dbo].[VoteTime]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VoteTime];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Candidates'
CREATE TABLE [dbo].[Candidates] (
    [MatNo] varchar(30)  NOT NULL,
    [FirstName] varchar(50)  NOT NULL,
    [LastName] varchar(50)  NOT NULL,
    [Sex] varchar(10)  NOT NULL,
    [email] varchar(10)  NULL,
    [Position] int  NOT NULL,
    [CurrentLevel] varchar(20)  NOT NULL,
    [Gpa] decimal(18,0)  NOT NULL,
    [Manifestor] varchar(200)  NOT NULL,
    [phoneNo] varchar(10)  NULL,
    [PhotoUrl] varchar(max)  NULL
);
GO

-- Creating table 'CandidateVotes'
CREATE TABLE [dbo].[CandidateVotes] (
    [CandidateMatNo] varchar(30)  NOT NULL,
    [VoterMatNo] varchar(30)  NOT NULL,
    [positionID] int  NOT NULL,
    [ID] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'CandidateVoteCounts'
CREATE TABLE [dbo].[CandidateVoteCounts] (
    [CandidateMatNo] varchar(30)  NOT NULL,
    [NoOfVotes] int  NOT NULL
);
GO

-- Creating table 'Positions'
CREATE TABLE [dbo].[Positions] (
    [PositionID] int IDENTITY(1,1) NOT NULL,
    [Position1] varchar(50)  NULL
);
GO

-- Creating table 'Voters'
CREATE TABLE [dbo].[Voters] (
    [MatNo] varchar(30)  NOT NULL,
    [FirstName] varchar(50)  NOT NULL,
    [LastName] varchar(50)  NOT NULL,
    [Sex] varchar(10)  NOT NULL,
    [Phone] varchar(15)  NOT NULL,
    [CurrentLevel] varchar(50)  NOT NULL,
    [email] varchar(50)  NULL,
    [VoteID] varchar(50)  NULL,
    [VoterCredential] varchar(max)  NULL
);
GO

-- Creating table 'Logins'
CREATE TABLE [dbo].[Logins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'UserClaims'
CREATE TABLE [dbo].[UserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] nvarchar(128)  NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [DateStamp] datetime  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'VoteTimes'
CREATE TABLE [dbo].[VoteTimes] (
    [VoteTimeStart] datetime  NULL,
    [VoteTimeEnd] datetime  NULL,
    [sn] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [Roles_Id] nvarchar(128)  NOT NULL,
    [Users_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MatNo] in table 'Candidates'
ALTER TABLE [dbo].[Candidates]
ADD CONSTRAINT [PK_Candidates]
    PRIMARY KEY CLUSTERED ([MatNo] ASC);
GO

-- Creating primary key on [ID] in table 'CandidateVotes'
ALTER TABLE [dbo].[CandidateVotes]
ADD CONSTRAINT [PK_CandidateVotes]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [CandidateMatNo] in table 'CandidateVoteCounts'
ALTER TABLE [dbo].[CandidateVoteCounts]
ADD CONSTRAINT [PK_CandidateVoteCounts]
    PRIMARY KEY CLUSTERED ([CandidateMatNo] ASC);
GO

-- Creating primary key on [PositionID] in table 'Positions'
ALTER TABLE [dbo].[Positions]
ADD CONSTRAINT [PK_Positions]
    PRIMARY KEY CLUSTERED ([PositionID] ASC);
GO

-- Creating primary key on [MatNo] in table 'Voters'
ALTER TABLE [dbo].[Voters]
ADD CONSTRAINT [PK_Voters]
    PRIMARY KEY CLUSTERED ([MatNo] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [PK_Logins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserClaims'
ALTER TABLE [dbo].[UserClaims]
ADD CONSTRAINT [PK_UserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [sn] in table 'VoteTimes'
ALTER TABLE [dbo].[VoteTimes]
ADD CONSTRAINT [PK_VoteTimes]
    PRIMARY KEY CLUSTERED ([sn] ASC);
GO

-- Creating primary key on [Roles_Id], [Users_Id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([Roles_Id], [Users_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Position] in table 'Candidates'
ALTER TABLE [dbo].[Candidates]
ADD CONSTRAINT [FK_Candidates_Positions]
    FOREIGN KEY ([Position])
    REFERENCES [dbo].[Positions]
        ([PositionID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Candidates_Positions'
CREATE INDEX [IX_FK_Candidates_Positions]
ON [dbo].[Candidates]
    ([Position]);
GO

-- Creating foreign key on [CandidateMatNo] in table 'CandidateVotes'
ALTER TABLE [dbo].[CandidateVotes]
ADD CONSTRAINT [FK_CandidateVote_Candidates]
    FOREIGN KEY ([CandidateMatNo])
    REFERENCES [dbo].[Candidates]
        ([MatNo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidateVote_Candidates'
CREATE INDEX [IX_FK_CandidateVote_Candidates]
ON [dbo].[CandidateVotes]
    ([CandidateMatNo]);
GO

-- Creating foreign key on [CandidateMatNo] in table 'CandidateVoteCounts'
ALTER TABLE [dbo].[CandidateVoteCounts]
ADD CONSTRAINT [FK_CandidateVoteCount_Candidates]
    FOREIGN KEY ([CandidateMatNo])
    REFERENCES [dbo].[Candidates]
        ([MatNo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [CandidateMatNo] in table 'CandidateVotes'
ALTER TABLE [dbo].[CandidateVotes]
ADD CONSTRAINT [FK_CandidateVote_CandidateVoteCount]
    FOREIGN KEY ([CandidateMatNo])
    REFERENCES [dbo].[CandidateVoteCounts]
        ([CandidateMatNo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidateVote_CandidateVoteCount'
CREATE INDEX [IX_FK_CandidateVote_CandidateVoteCount]
ON [dbo].[CandidateVotes]
    ([CandidateMatNo]);
GO

-- Creating foreign key on [positionID] in table 'CandidateVotes'
ALTER TABLE [dbo].[CandidateVotes]
ADD CONSTRAINT [FK_CandidateVote_Positions]
    FOREIGN KEY ([positionID])
    REFERENCES [dbo].[Positions]
        ([PositionID])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidateVote_Positions'
CREATE INDEX [IX_FK_CandidateVote_Positions]
ON [dbo].[CandidateVotes]
    ([positionID]);
GO

-- Creating foreign key on [VoterMatNo] in table 'CandidateVotes'
ALTER TABLE [dbo].[CandidateVotes]
ADD CONSTRAINT [FK_CandidateVote_Voters]
    FOREIGN KEY ([VoterMatNo])
    REFERENCES [dbo].[Voters]
        ([MatNo])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CandidateVote_Voters'
CREATE INDEX [IX_FK_CandidateVote_Voters]
ON [dbo].[CandidateVotes]
    ([VoterMatNo]);
GO

-- Creating foreign key on [UserId] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [FK_dbo_Logins_dbo_Users_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_Logins_dbo_Users_UserId'
CREATE INDEX [IX_FK_dbo_Logins_dbo_Users_UserId]
ON [dbo].[Logins]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserClaims'
ALTER TABLE [dbo].[UserClaims]
ADD CONSTRAINT [FK_dbo_UserClaims_dbo_Users_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_UserClaims_dbo_Users_UserId'
CREATE INDEX [IX_FK_dbo_UserClaims_dbo_Users_UserId]
ON [dbo].[UserClaims]
    ([UserId]);
GO

-- Creating foreign key on [Roles_Id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRoles_Role]
    FOREIGN KEY ([Roles_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_Id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRoles_User]
    FOREIGN KEY ([Users_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRoles_User'
CREATE INDEX [IX_FK_UserRoles_User]
ON [dbo].[UserRoles]
    ([Users_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------