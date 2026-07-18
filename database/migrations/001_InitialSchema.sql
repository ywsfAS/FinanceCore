
/****** Object:  Table [dbo].[Accounts] ******/
CREATE TABLE [dbo].[Accounts](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[AccountTypeId] [tinyint] NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[CurrencyId] [tinyint] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NOT NULL,
	[InitialBalance] [decimal](18, 2) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK__Accounts__3214EC071A5117EF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Accounts_UserId_Name] UNIQUE NONCLUSTERED 
(
	[UserId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[AccountTypes] ******/

CREATE TABLE [dbo].[AccountTypes](
	[AccountTypeId] [tinyint] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AccountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[AuditLogs] ******/
CREATE TABLE [dbo].[AuditLogs](
	[Id] [uniqueidentifier] NOT NULL,
	[Action] [nvarchar](255) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[EntityName] [nvarchar](255) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[BudgetPeriods] ******/
CREATE TABLE [dbo].[BudgetPeriods](
	[BudgetPeriodId] [tinyint] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BudgetPeriodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Budgets] ******/
CREATE TABLE [dbo].[Budgets](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[CurrencyId] [tinyint] NOT NULL,
	[BudgetPeriodId] [tinyint] NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK__Budgets__3214EC07D4ABA02C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Budgets_UserId_Name] UNIQUE NONCLUSTERED 
(
	[UserId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Categories] ******/
CREATE TABLE [dbo].[Categories](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CategoryTypeId] [tinyint] NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsActive] [bit] NULL,
	[IsDefault] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Categories_UserId_Name] UNIQUE NONCLUSTERED 
(
	[UserId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[CategoryTypes] ******/
CREATE TABLE [dbo].[CategoryTypes](
	[CategoryTypeId] [tinyint] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[ContactMessages] ******/
CREATE TABLE [dbo].[ContactMessages](
	[Id] [uniqueidentifier] NOT NULL,
	[FullName] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[SubjectId] [tinyint] NOT NULL,
	[CreatedAt] [datetime2](7) NULL,
	[IsProccessed] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

/****** Object:  Table [dbo].[Contributions] ******/
CREATE TABLE [dbo].[Contributions](
	[Id] [uniqueidentifier] NOT NULL,
	[SavingGoalId] [uniqueidentifier] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[CurrencyId] [tinyint] NOT NULL,
	[Description] [nvarchar](255) NULL,
	[Date] [datetime2](7) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[ContributionsTypes] ******/
CREATE TABLE [dbo].[ContributionsTypes](
	[Id] [tinyint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Currencies] ******/
CREATE TABLE [dbo].[Currencies](
	[CurrencyId] [tinyint] NOT NULL,
	[Code] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CurrencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[ExchangeRates] ******/
CREATE TABLE [dbo].[ExchangeRates](
	[Id] [uniqueidentifier] NOT NULL,
	[SourceCurrencyId] [tinyint] NOT NULL,
	[TargetCurrencyId] [tinyint] NOT NULL,
	[Rate] [decimal](18, 6) NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_ExchangeRate] UNIQUE NONCLUSTERED 
(
	[SourceCurrencyId] ASC,
	[TargetCurrencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[GoalStatuses] ******/
CREATE TABLE [dbo].[GoalStatuses](
	[Id] [tinyint] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[MessageSubjects] ******/
CREATE TABLE [dbo].[MessageSubjects](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[PasswordResetTokens] ******/
CREATE TABLE [dbo].[PasswordResetTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Token] [nvarchar](200) NOT NULL,
	[ExpiresAt] [datetime2](7) NOT NULL,
	[IsUsed] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Profiles] ******/
CREATE TABLE [dbo].[Profiles](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Bio] [nvarchar](500) NULL,
	[AvatarUrl] [nvarchar](500) NULL,
	[Currency] [tinyint] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_Profiles_UserId] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[RecurringTransactions] ******/
CREATE TABLE [dbo].[RecurringTransactions](
	[Id] [uniqueidentifier] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](255) NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NULL,
	[Period] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[LastExecutedDate] [datetime2](7) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[Type] [tinyint] NOT NULL,
 CONSTRAINT [PK__Recurrin__3214EC078B551391] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[SavingsGoals] ******/
CREATE TABLE [dbo].[SavingsGoals](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[CurrencyId] [tinyint] NOT NULL,
	[TargetAmount] [decimal](18, 2) NOT NULL,
	[CurrentAmount] [decimal](18, 2) NOT NULL,
	[TargetDate] [datetime2](7) NULL,
	[StatusId] [tinyint] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[CompletedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Transactions] ******/
CREATE TABLE [dbo].[Transactions](
	[Id] [uniqueidentifier] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
	[ToAccountId] [uniqueidentifier] NULL,
	[CategoryId] [uniqueidentifier] NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TransactionTypeId] [tinyint] NOT NULL,
	[Date] [datetime2](7) NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[CurrencyId] [tinyint] NOT NULL,
 CONSTRAINT [PK__Transact__3214EC07EE222469] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[TransactionTypes] ******/
CREATE TABLE [dbo].[TransactionTypes](
	[TransactionTypeId] [tinyint] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TransactionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Table [dbo].[Users] ******/
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PasswordHash] [nvarchar](500) NOT NULL,
	[TimeZone] [nvarchar](100) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

/****** Object:  Index [IX_Accounts_AccountTypeId] ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_AccountTypeId] ON [dbo].[Accounts]
(
	[AccountTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Accounts_CurrencyId] ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_CurrencyId] ON [dbo].[Accounts]
(
	[CurrencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Accounts_UserId] ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_UserId] ON [dbo].[Accounts]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Budgets_BudgetPeriodId] ******/
CREATE NONCLUSTERED INDEX [IX_Budgets_BudgetPeriodId] ON [dbo].[Budgets]
(
	[BudgetPeriodId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Budgets_CategoryId] ******/
CREATE NONCLUSTERED INDEX [IX_Budgets_CategoryId] ON [dbo].[Budgets]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Budgets_CurrencyId] ******/
CREATE NONCLUSTERED INDEX [IX_Budgets_CurrencyId] ON [dbo].[Budgets]
(
	[CurrencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Budgets_StartDate_EndDate] ******/
CREATE NONCLUSTERED INDEX [IX_Budgets_StartDate_EndDate] ON [dbo].[Budgets]
(
	[StartDate] ASC,
	[EndDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Budgets_UserId] ******/
CREATE NONCLUSTERED INDEX [IX_Budgets_UserId] ON [dbo].[Budgets]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Categories_CategoryTypeId] ******/
CREATE NONCLUSTERED INDEX [IX_Categories_CategoryTypeId] ON [dbo].[Categories]
(
	[CategoryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Categories_UserId] ******/
CREATE NONCLUSTERED INDEX [IX_Categories_UserId] ON [dbo].[Categories]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_PasswordResetTokens_Token] ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_PasswordResetTokens_Token] ON [dbo].[PasswordResetTokens]
(
	[Token] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Profiles_UserId] ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Profiles_UserId] ON [dbo].[Profiles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_RecurringTransactions_AccountId] ******/
CREATE NONCLUSTERED INDEX [IX_RecurringTransactions_AccountId] ON [dbo].[RecurringTransactions]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_RecurringTransactions_CategoryId] ******/
CREATE NONCLUSTERED INDEX [IX_RecurringTransactions_CategoryId] ON [dbo].[RecurringTransactions]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_RecurringTransactions_IsActive] ******/
CREATE NONCLUSTERED INDEX [IX_RecurringTransactions_IsActive] ON [dbo].[RecurringTransactions]
(
	[IsActive] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_SavingsGoals_UserId] ******/
CREATE NONCLUSTERED INDEX [IX_SavingsGoals_UserId] ON [dbo].[SavingsGoals]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_SavingsGoals_UserId_Status] ******/
CREATE NONCLUSTERED INDEX [IX_SavingsGoals_UserId_Status] ON [dbo].[SavingsGoals]
(
	[UserId] ASC,
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Transactions_AccountId] ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_AccountId] ON [dbo].[Transactions]
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Transactions_CategoryId] ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_CategoryId] ON [dbo].[Transactions]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Transactions_Date] ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_Date] ON [dbo].[Transactions]
(
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Transactions_ToAccountId] ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_ToAccountId] ON [dbo].[Transactions]
(
	[ToAccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Transactions_TransactionTypeId] ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_TransactionTypeId] ON [dbo].[Transactions]
(
	[TransactionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

/****** Object:  Index [IX_Users_Email] ******/
CREATE NONCLUSTERED INDEX [IX_Users_Email] ON [dbo].[Users]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]

ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF__Accounts__Id__04E4BC85]  DEFAULT (newid()) FOR [Id]

ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [DF__Accounts__Create__05D8E0BE]  DEFAULT (getutcdate()) FOR [CreatedAt]

ALTER TABLE [dbo].[Budgets] ADD  CONSTRAINT [DF__Budgets__Id__18EBB532]  DEFAULT (newid()) FOR [Id]

ALTER TABLE [dbo].[Budgets] ADD  CONSTRAINT [DF__Budgets__Created__19DFD96B]  DEFAULT (getutcdate()) FOR [CreatedAt]

ALTER TABLE [dbo].[Categories] ADD  DEFAULT (newid()) FOR [Id]

ALTER TABLE [dbo].[Categories] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]

ALTER TABLE [dbo].[ContactMessages] ADD  DEFAULT (newid()) FOR [Id]

ALTER TABLE [dbo].[ContactMessages] ADD  DEFAULT (sysdatetime()) FOR [CreatedAt]

ALTER TABLE [dbo].[ContactMessages] ADD  CONSTRAINT [DF_ContactMessages_IsProccessed]  DEFAULT ((0)) FOR [IsProccessed]

ALTER TABLE [dbo].[Contributions] ADD  DEFAULT (newid()) FOR [Id]

ALTER TABLE [dbo].[Contributions] ADD  DEFAULT (sysutcdatetime()) FOR [Date]

ALTER TABLE [dbo].[Contributions] ADD  DEFAULT (sysutcdatetime()) FOR [CreatedAt]

ALTER TABLE [dbo].[PasswordResetTokens] ADD  DEFAULT (newid()) FOR [Id]

ALTER TABLE [dbo].[PasswordResetTokens] ADD  DEFAULT ((0)) FOR [IsUsed]

ALTER TABLE [dbo].[PasswordResetTokens] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]

ALTER TABLE [dbo].[Profiles] ADD  DEFAULT (newid()) FOR [Id]

ALTER TABLE [dbo].[Profiles] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]

ALTER TABLE [dbo].[RecurringTransactions] ADD  CONSTRAINT [DF__Recurring__IsAct__22401542]  DEFAULT ((1)) FOR [IsActive]

ALTER TABLE [dbo].[RecurringTransactions] ADD  CONSTRAINT [DF__Recurring__Creat__2334397B]  DEFAULT (getutcdate()) FOR [CreatedAt]

ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF__Transactions__Id__4F47C5E3]  DEFAULT (newid()) FOR [Id]

ALTER TABLE [dbo].[Transactions] ADD  CONSTRAINT [DF__Transacti__Creat__503BEA1C]  DEFAULT (getutcdate()) FOR [CreatedAt]

ALTER TABLE [dbo].[Users] ADD  DEFAULT (newid()) FOR [Id]

ALTER TABLE [dbo].[Users] ADD  DEFAULT (getutcdate()) FOR [CreatedAt]
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_AccountTypes] FOREIGN KEY([AccountTypeId])
REFERENCES [dbo].[AccountTypes] ([AccountTypeId])
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_AccountTypes]
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Currencies] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currencies] ([CurrencyId])
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Currencies]
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Users]
ALTER TABLE [dbo].[Budgets]  WITH CHECK ADD  CONSTRAINT [FK_Budgets_BudgetPeriods] FOREIGN KEY([BudgetPeriodId])
REFERENCES [dbo].[BudgetPeriods] ([BudgetPeriodId])
ALTER TABLE [dbo].[Budgets] CHECK CONSTRAINT [FK_Budgets_BudgetPeriods]
ALTER TABLE [dbo].[Budgets]  WITH CHECK ADD  CONSTRAINT [FK_Budgets_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ALTER TABLE [dbo].[Budgets] CHECK CONSTRAINT [FK_Budgets_Categories]
ALTER TABLE [dbo].[Budgets]  WITH CHECK ADD  CONSTRAINT [FK_Budgets_Currencies] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currencies] ([CurrencyId])
ALTER TABLE [dbo].[Budgets] CHECK CONSTRAINT [FK_Budgets_Currencies]
ALTER TABLE [dbo].[Budgets]  WITH CHECK ADD  CONSTRAINT [FK_Budgets_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
ALTER TABLE [dbo].[Budgets] CHECK CONSTRAINT [FK_Budgets_Users]
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_CategoryTypes] FOREIGN KEY([CategoryTypeId])
REFERENCES [dbo].[CategoryTypes] ([CategoryTypeId])
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_CategoryTypes]
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Users]
ALTER TABLE [dbo].[ContactMessages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_MessageSubjects] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[MessageSubjects] ([Id])
ALTER TABLE [dbo].[ContactMessages] CHECK CONSTRAINT [FK_Messages_MessageSubjects]
ALTER TABLE [dbo].[Contributions]  WITH CHECK ADD  CONSTRAINT [FK_Contributions_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ALTER TABLE [dbo].[Contributions] CHECK CONSTRAINT [FK_Contributions_Accounts]
ALTER TABLE [dbo].[Contributions]  WITH CHECK ADD  CONSTRAINT [FK_Contributions_ContributionsTypes] FOREIGN KEY([Type])
REFERENCES [dbo].[ContributionsTypes] ([Id])
ALTER TABLE [dbo].[Contributions] CHECK CONSTRAINT [FK_Contributions_ContributionsTypes]
ALTER TABLE [dbo].[Contributions]  WITH CHECK ADD  CONSTRAINT [FK_Contributions_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currencies] ([CurrencyId])
ALTER TABLE [dbo].[Contributions] CHECK CONSTRAINT [FK_Contributions_Currency]
ALTER TABLE [dbo].[Contributions]  WITH CHECK ADD  CONSTRAINT [FK_Contributions_Savings] FOREIGN KEY([SavingGoalId])
REFERENCES [dbo].[SavingsGoals] ([Id])
ALTER TABLE [dbo].[Contributions] CHECK CONSTRAINT [FK_Contributions_Savings]
ALTER TABLE [dbo].[ExchangeRates]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeRates_SourceCurrency] FOREIGN KEY([SourceCurrencyId])
REFERENCES [dbo].[Currencies] ([CurrencyId])
ALTER TABLE [dbo].[ExchangeRates] CHECK CONSTRAINT [FK_ExchangeRates_SourceCurrency]
ALTER TABLE [dbo].[ExchangeRates]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeRates_TargetCurrency] FOREIGN KEY([TargetCurrencyId])
REFERENCES [dbo].[Currencies] ([CurrencyId])
ALTER TABLE [dbo].[ExchangeRates] CHECK CONSTRAINT [FK_ExchangeRates_TargetCurrency]
ALTER TABLE [dbo].[PasswordResetTokens]  WITH CHECK ADD  CONSTRAINT [FK_PasswordResetTokens_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[PasswordResetTokens] CHECK CONSTRAINT [FK_PasswordResetTokens_Users]
ALTER TABLE [dbo].[Profiles]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_Currency] FOREIGN KEY([Currency])
REFERENCES [dbo].[Currencies] ([CurrencyId])
ALTER TABLE [dbo].[Profiles] CHECK CONSTRAINT [FK_Profiles_Currency]
ALTER TABLE [dbo].[Profiles]  WITH CHECK ADD  CONSTRAINT [FK_Profiles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[Profiles] CHECK CONSTRAINT [FK_Profiles_Users]
ALTER TABLE [dbo].[RecurringTransactions]  WITH CHECK ADD  CONSTRAINT [FK_RecurringTransactions_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ALTER TABLE [dbo].[RecurringTransactions] CHECK CONSTRAINT [FK_RecurringTransactions_Account]
ALTER TABLE [dbo].[RecurringTransactions]  WITH CHECK ADD  CONSTRAINT [FK_RecurringTransactions_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ALTER TABLE [dbo].[RecurringTransactions] CHECK CONSTRAINT [FK_RecurringTransactions_Category]
ALTER TABLE [dbo].[RecurringTransactions]  WITH CHECK ADD  CONSTRAINT [FK_Type] FOREIGN KEY([Type])
REFERENCES [dbo].[TransactionTypes] ([TransactionTypeId])
ALTER TABLE [dbo].[RecurringTransactions] CHECK CONSTRAINT [FK_Type]
ALTER TABLE [dbo].[SavingsGoals]  WITH CHECK ADD  CONSTRAINT [FK_SavingGoals_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currencies] ([CurrencyId])
ALTER TABLE [dbo].[SavingsGoals] CHECK CONSTRAINT [FK_SavingGoals_Currency]
ALTER TABLE [dbo].[SavingsGoals]  WITH CHECK ADD  CONSTRAINT [FK_SavingGoals_GoalStatus] FOREIGN KEY([StatusId])
REFERENCES [dbo].[GoalStatuses] ([Id])
ALTER TABLE [dbo].[SavingsGoals] CHECK CONSTRAINT [FK_SavingGoals_GoalStatus]
ALTER TABLE [dbo].[SavingsGoals]  WITH CHECK ADD  CONSTRAINT [FK_SavingsGoals_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ALTER TABLE [dbo].[SavingsGoals] CHECK CONSTRAINT [FK_SavingsGoals_Users]
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_CurrencyId_tx] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currencies] ([CurrencyId])
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_CurrencyId_tx]
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Accounts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Accounts] ([Id])
ON DELETE CASCADE
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Accounts]
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Categories]
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_ToAccounts] FOREIGN KEY([ToAccountId])
REFERENCES [dbo].[Accounts] ([Id])
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_ToAccounts]
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_TransactionTypes] FOREIGN KEY([TransactionTypeId])
REFERENCES [dbo].[TransactionTypes] ([TransactionTypeId])
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_TransactionTypes]