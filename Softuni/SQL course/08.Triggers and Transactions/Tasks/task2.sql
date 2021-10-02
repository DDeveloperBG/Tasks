--USE Bank

CREATE TABLE NotificationEmails(
	Id INT IDENTITY PRIMARY KEY,
	Recipient INT NOT NULL,
	[Subject] VARCHAR(40) NOT NULL,
	Body  VARCHAR(70) NOT NULL
)

GO

CREATE TRIGGER TR_Logs_AfterUpdate
ON Logs AFTER INSERT
AS
	INSERT INTO NotificationEmails
		SELECT AccountId AS Recipient, 
				CONCAT('Balance change for account: ', AccountId) AS [Subject], 
				CONCAT('On ', GETDATE(), 
					' your balance was changed from ', 
					OldSum, ' to ', NewSum, '.') AS Body
			FROM inserted