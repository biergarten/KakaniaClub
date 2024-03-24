USE [mas_db]
GO
DELETE FROM [dbo].[Applications]
GO
INSERT [dbo].[Applications] ([Id], [AssignToUserId], [EmailLocation], [DateInitiated], [Person_Name_FirstName], [Person_Name_LastName], [Person_Email], [Person_Phone], [MembershipType], [Status], [ReferralProcessInfo_ReferralStatus], [ReferralProcessInfo_HasEmailBeenMoved], [ReferralProcessInfo_EmailOriginalLocation], [ReferralProcessInfo_EmailNewLocation]) VALUES (N'22ae1357-d3e3-4737-a773-01d9830c1bca', N'', N'22 email location', CAST(N'2024-03-17T00:00:00.0000000' AS DateTime2), N'juanillo', N'gaballa', N'juanillo.gabarra@gmail.com', N'22-9889-09', 1, 0, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[Applications] ([Id], [AssignToUserId], [EmailLocation], [DateInitiated], [Person_Name_FirstName], [Person_Name_LastName], [Person_Email], [Person_Phone], [MembershipType], [Status], [ReferralProcessInfo_ReferralStatus], [ReferralProcessInfo_HasEmailBeenMoved], [ReferralProcessInfo_EmailOriginalLocation], [ReferralProcessInfo_EmailNewLocation]) VALUES (N'33ae1357-d3e3-4737-a773-01d9830c1bca', N'', N'33 email location', CAST(N'2024-03-17T00:00:00.0000000' AS DateTime2), N'anton', N'rodes', N'anton.rodes@gmail.com', N'33-11111', 1, 0, NULL, NULL, NULL, NULL)
GO
