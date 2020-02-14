CREATE DATABASE BlackSound
GO

USE BlackSound
GO

CREATE TABLE [Users] (
  [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  [Email] VARCHAR(255) NOT NULL,
  [Password] VARCHAR(255) NOT NULL,
  [FullName] VARCHAR(255) NOT NULL,
  [IsAdmin] BIT NOT NULL
)
GO

CREATE TABLE [PlayLists] (
  [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  [ParentId] INT REFERENCES [Users]([Id]) NOT NULL,
  [Name] VARCHAR(255) NOT NULL,
  [Description] VARCHAR(255) NOT NULL,
  [IsPublic] BIT NOT NULL
)
GO

CREATE TABLE [Songs] (
  [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
  [ParentId] INT REFERENCES [PlayLists]([Id]) NOT NULL,
  [Title] VARCHAR(255) NOT NULL,
  [Artist] VARCHAR(255) NOT NULL,
  [Year] INT NOT NULL
)
GO

CREATE TABLE [UserToPlayList] (
  [UserId] INT REFERENCES [Users](Id) NOT NULL,
  [PlayListId] INT REFERENCES [PlayLists](Id) NOT NULL,
  PRIMARY KEY ([UserId], [PlayListId])
)
GO

INSERT INTO [Users] ([Email], [FullName], [Password], [IsAdmin])
VALUES ('nvalchanov@gmail.com', 'Nikola Valchanov', 'nikipass', 1)


select * from users
SELECT SCOPE_IDENTITY()