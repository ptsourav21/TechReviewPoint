CREATE TABLE [dbo].[Admins]
(
   [AdminID] INT IDENTITY(1,1) PRIMARY KEY,
   [AdminEmail] NVARCHAR(50) NOT NULL UNIQUE, 
   [AdminName] NVARCHAR(50),  
   [AdminImg] NVARCHAR(MAX),
   [AdminPassword] NVARCHAR(50) NOT NULL    
)

CREATE TABLE [dbo].[Users]
(
   [UserID] INT IDENTITY(1,1) PRIMARY KEY, 
   [UserName] NVARCHAR(50) NOT NULL,
   [UserEmail] NVARCHAR(50) NOT NULL UNIQUE, 
   [UserPassword] NVARCHAR(50) NOT NULL,    
   [UserPhone] INT NOT NULL UNIQUE CHECK(UserPhone>1000000000), 
   [UserAdress] NVARCHAR(MAX),
   [UserImg] NVARCHAR(MAX),
   [IsEmailVerified] BIT,
   [ActivationCode] UNIQUEIDENTIFIER,
   [tp_point] DECIMAL(5, 2) 
)

CREATE TABLE [dbo].[Pendings]
(
   [PendingID] INT IDENTITY(1,1) PRIMARY KEY, 
   [UserID] INT NOT NULL FOREIGN KEY REFERENCES Users(UserID),
   [Mail] NVARCHAR(MAX),
   [Date] DATE
)

CREATE TABLE [dbo].[Categories]
(
   [CategoryID] INT IDENTITY(1,1) PRIMARY KEY, 
   [CategoryName] NVARCHAR(50) NOT NULL
)

CREATE TABLE [dbo].[Products]
(
   [ProductID] INT IDENTITY(1,1) PRIMARY KEY, 
   [CategoryID] INT NOT NULL FOREIGN KEY REFERENCES Categories(CategoryID),
   [ProductName] NVARCHAR(50) NOT NULL, 
   [ProductPrice] DECIMAL(18, 2) NOT NULL, 
   [ProductDetails] NVARCHAR(MAX),
   [ProductImg] NVARCHAR(MAX)
  
)

CREATE TABLE [dbo].[Reviews] 
( 
   [ReviewID] INT IDENTITY(1,1) PRIMARY KEY, 
   [review_point] INT DEFAULT 0,
   [UserID] INT NOT NULL FOREIGN KEY REFERENCES Users(UserID), 
   [ProductID] INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID),  
   [ReviewPost] NVARCHAR(MAX),
   [ReviewDate] DATE,
   [Picture] NVARCHAR(MAX),
)

CREATE TABLE [dbo].[Comments] 
( 
   [CommentID] INT IDENTITY(1,1) PRIMARY KEY, 
  
   [UserID] INT NOT NULL FOREIGN KEY REFERENCES Users(UserID), 
   [ReviewID] INT NOT NULL FOREIGN KEY REFERENCES Reviews(ReviewID),  
   [Comment] NVARCHAR(MAX),
   [CommentDate] DATE
)

CREATE TABLE [dbo].[Issues] 
( 
   [IssueID] INT IDENTITY(1,1) PRIMARY KEY, 
  
   [UserID] INT NOT NULL FOREIGN KEY REFERENCES Users(UserID), 
   [ProductID] INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID),  
   [IssuePost] NVARCHAR(MAX),
   [IssueDate] DATE
)

CREATE TABLE [dbo].[Replies] 
( 
   [ReplyID] INT IDENTITY(1,1) PRIMARY KEY, 
  
   [UserID] INT NOT NULL FOREIGN KEY REFERENCES Users(UserID), 
   [IssueID] INT NOT NULL FOREIGN KEY REFERENCES Issues(IssueID),  
   [Reply] NVARCHAR(MAX),
   [ReplyDate] DATE
)

CREATE TABLE [dbo].[Questions] 
( 
   [QuestionID] INT IDENTITY(1,1) PRIMARY KEY, 
   [UserID] INT NOT NULL FOREIGN KEY REFERENCES Users(UserID), 

   [Problem] NVARCHAR(MAX),
   [Status] INT DEFAULT 0,
   [QuestionDate] DATE
)

CREATE TABLE [dbo].[Answers] 
( 
   [AnswerID] INT IDENTITY(1,1) PRIMARY KEY, 
   [UserID] INT NOT NULL FOREIGN KEY REFERENCES Users(UserID), 
   [QuestionID] INT NOT NULL FOREIGN KEY REFERENCES Questions(QuestionID),  
   [Solve] NVARCHAR(MAX),
   [AnswerDate] DATE
)

CREATE TABLE [dbo].[WishLists] 
( 
   [WishID] INT IDENTITY(1,1) PRIMARY KEY, 
   [Quantity] INT DEFAULT 0,
   [UserID] INT NOT NULL FOREIGN KEY REFERENCES Users(UserID), 
   [ProductID] INT NOT NULL FOREIGN KEY REFERENCES Products(ProductID),  
   [Price] DECIMAL(10, 2) NOT NULL, 
   [WishDate] DATE
)

CREATE TABLE [dbo].[Followers] 
(
 [FollwerID] INT IDENTITY(1,1) PRIMARY KEY,
 [UserID] INT NOT NULL FOREIGN KEY REFERENCES Users(UserID)

) 
