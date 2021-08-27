-- REMOVE CURRENT PRIMARY KEY AND THEN CREATE NEW PRIMARY KEY(COMBINATION OF Id + Username) ON DATABASE Users, task 9

USE Users

ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC07D79A9161 -- the constraint name was in the table keys folder

ALTER TABLE Users
ADD CONSTRAINT PK_Users_CompositeIdUsername
PRIMARY KEY(Id, Username)