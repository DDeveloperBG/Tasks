USE Bank

CREATE TABLE Logs (
	LogId INT IDENTITY PRIMARY KEY, 
	AccountId INT NOT NULL REFERENCES Accounts(Id), 
	OldSum Money NOT NULL, 
	NewSum Money NOT NULL
)

GO

CREATE TRIGGER TR_Accounts_AfterUpdate
ON Accounts AFTER UPDATE
AS
	INSERT INTO Logs
		SELECT d.AccountHolderId AS	AccountId, 
				d.Balance AS OldSum,
				i.Balance AS NewSum
			FROM deleted d
			JOIN inserted i ON i.Id = d.Id