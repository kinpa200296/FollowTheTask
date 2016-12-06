INSERT INTO [dbo].[Roles]([Name])
    VALUES ('Admin'), ('Leader'), ('Developer'), ('User')

INSERT INTO [dbo].[ActionTypes]([Name])
    VALUES ('Approved'), ('Declined')

INSERT INTO [dbo].[IssueTypes]([Name])
    VALUES ('Story'), ('Improvement'), ('Bug'), ('Investigation')

INSERT INTO [dbo].[Priorities]([Name])
    VALUES ('Trivial'), ('Minor'), ('Normal'), ('Major'), ('Critical')

INSERT INTO [dbo].[Resolutions]([Name])
    VALUES ('Unknown'), ('CannotReproduce'), ('Done'), ('Fixed')

INSERT INTO [dbo].[Statuses]([Name])
    VALUES ('Open'), ('Reopened'), ('InProgress'), ('Resolved'), ('Closed')
