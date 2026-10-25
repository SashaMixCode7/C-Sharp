SET IDENTITY_INSERT [dbo].[Good] ON
INSERT INTO [dbo].[Good] ([Id], [Name], [Description], [Image_link], [Price]) VALUES (1, N'Мильфей', N'Десерт из слоеного теста с клубникой и сырным кремом. 175 гр.', N'jdhfghd', CAST(440.00 AS Decimal(18, 2)))
INSERT INTO [dbo].[Good] ([Id], [Name], [Description], [Image_link], [Price]) VALUES (2, N'Прага ', N'Шоколадный бисквит, масляно-шоколадный крем, шоколадный гляссаж. 120 гр.', N'ghgfdgf', CAST(280.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Good] OFF
