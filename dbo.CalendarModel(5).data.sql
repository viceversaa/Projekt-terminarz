SET IDENTITY_INSERT [dbo].[CalendarModel] ON
INSERT INTO [dbo].[CalendarModel] ([ID], [typ_zajec], [pracownik], [dzien_data], [godzina_od], [godzina_do], [status]) VALUES (1, N'lonża', N'Sara', '2023-05-16',N'16:00', N'16:30', N'wolna')
INSERT INTO [dbo].[CalendarModel] ([ID], [typ_zajec], [pracownik], [dzien_data], [godzina_od], [godzina_do], [status]) VALUES (2, N'lonża', N'Sara', '2023-05-16',N'16:30', N'17:00', N'zajęta')
INSERT INTO [dbo].[CalendarModel] ([ID], [typ_zajec], [pracownik], [dzien_data], [godzina_od], [godzina_do], [status]) VALUES (3, N'grupa', N'Weronika', '2023-05-16',N'16:00', N'17:00', N'wolna')
SET IDENTITY_INSERT [dbo].[CalendarModel] OFF
