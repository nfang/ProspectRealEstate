SET IDENTITY_INSERT [dbo].[Languages] ON
INSERT INTO [dbo].[Languages] ([ID], [lang_name], [display_name], [iso_639x]) VALUES (1, N'en-AU', N'English - Australia', N'ENA')
INSERT INTO [dbo].[Languages] ([ID], [lang_name], [display_name], [iso_639x]) VALUES (2, N'en-GB', N'English - United Kingdom', N'ENG')
INSERT INTO [dbo].[Languages] ([ID], [lang_name], [display_name], [iso_639x]) VALUES (3, N'en-US', N'English - United States', N'ENU')
INSERT INTO [dbo].[Languages] ([ID], [lang_name], [display_name], [iso_639x]) VALUES (4, N'zh-CN', N'Chinese - China', N'CHS')
INSERT INTO [dbo].[Languages] ([ID], [lang_name], [display_name], [iso_639x]) VALUES (5, N'zh-TW', N'Chinese - Taiwan', N'CHT')
INSERT INTO [dbo].[Languages] ([ID], [lang_name], [display_name], [iso_639x]) VALUES (6, N'zh-CHS', N'Chinese (Simplified)', NULL)
INSERT INTO [dbo].[Languages] ([ID], [lang_name], [display_name], [iso_639x]) VALUES (7, N'zh-CHT', N'Chinese (Traditional)', NULL)
SET IDENTITY_INSERT [dbo].[Languages] OFF

SET IDENTITY_INSERT [dbo].[Countries] ON
INSERT INTO [dbo].[Countries] ([ID], [country_code], [telephone_code], [common_name], [formal_name], [lang]) VALUES (1, N'AU', N'61', N'Australia', N'Commonwealth of Australia', 1)
SET IDENTITY_INSERT [dbo].[Countries] OFF

SET IDENTITY_INSERT [dbo].[States] ON
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (1, N'Australian Capital Territory', 1)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (2, N'New South Wales', 1)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (3, N'Northen Territory', 1)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (4, N'Queensland', 1)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (5, N'South Australia', 1)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (6, N'Tasmania', 1)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (7, N'Victoria', 1)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (8, N'Western Australia', 1)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (9, N'首都地区', 4)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (10, N'新南威尔士州', 4)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (11, N'北部地区', 4)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (12, N'昆士兰州', 4)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (13, N'南澳大利亚州', 4)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (14, N'塔斯马尼亚州', 4)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (15, N'维多利亚州', 4)
INSERT INTO [dbo].[States] ([ID], [state_name], [lang]) VALUES (16, N'西澳大利亚州', 4)
SET IDENTITY_INSERT [dbo].[States] OFF

INSERT INTO [dbo].[Users] ([ID], [login], [pass], [display_name], [email], [registered_on]) VALUES (1, N'admin', N'Admin1124', N'Administrator', N'admin@prospectrealestate.com.au', N'2012-11-22 00:00:00')

SET IDENTITY_INSERT [dbo].[Pages] ON
INSERT INTO [dbo].[Pages] ([ID], [name], [title], [content], [added_by], [added_on], [modified_by], [modified_on], [lang]) VALUES (1, N'about-us', N'About Us', N'Prospect Real
Estate &amp; Business Broker is highly experienced in the real estate and
business brokerage industry. <br>

<br>We understand our
clients come from a diverse background therefore our agents can speak fluent
English, Mandarin as well as Cantonese. <br>

<span><br>We are passionate
about setting high standards and raising the benchmark in customer service. Through
our core beliefs of honesty, fairness, professionalism and attention to detail
we constantly strive to achieve the best possible result for our clients. Our
aim is to establish a rapport with our customers not just for the duration of
the business deal but create an ongoing relationship well into the future.</span>', 1, N'2012-11-22 00:00:00', NULL, NULL, 1)
INSERT INTO [dbo].[Pages] ([ID], [name], [title], [content], [added_by], [added_on], [modified_by], [modified_on], [lang]) VALUES (2, N'about-us', N'关于我们', N'Prospect Real Estate是一个组织。这是中文版。', 1, N'2012-12-10 00:00:00', NULL, NULL, 4)
INSERT INTO [dbo].[Pages] ([ID], [name], [title], [content], [added_by], [added_on], [modified_by], [modified_on], [lang]) VALUES (3, N'finance', N'Finance', N'<p>Finance</p>', 1, N'2012-12-10 00:00:00', NULL, NULL, 1)
INSERT INTO [dbo].[Pages] ([ID], [name], [title], [content], [added_by], [added_on], [modified_by], [modified_on], [lang]) VALUES (4, N'finance', N'购房贷款', N'<p>购房贷款</p>', 1, N'2012-12-10 00:00:00', NULL, NULL, 4)
INSERT INTO [dbo].[Pages] ([ID], [name], [title], [content], [added_by], [added_on], [modified_by], [modified_on], [lang]) VALUES (5, N'privacy', N'Privacy Policy', N'At Prospect Real
Estate &amp; Business Broker, we are committed to ensuring the privacy of your
personal information.&nbsp;<br><br><b>How we collect personal
information
</b>
<br><br>Generally
we collect personal information directly from you in person, by telephone, letter,
fax, email or when you provide it to us through our websites. Sometimes we will
collect your personal information from third parties. For example, we may need
to collect personal information from a credit reporting agency, your
representative (such as a legal adviser), your financial adviser, your employer
or publicly available sources of information or any other organisations where
you have given your consent. <br>

<br><b>What personal information
we collect
</b>
<br><br>Generally
we collect your name, address, telephone numbers, email addresses, occupation,
financial information and any other information about you to enable us to
assess your needs in relation to real estate or business brokerage. <br>

<br><b>Disclosure of information
</b>
<br><br>We
may disclose your personal information as permitted or required by law, to
regulatory bodies and law enforcement and to third party service providers.
Where we act as agent in collection of your personal information we collect the
information on behalf of the parties for whom we act as agent both for our purposes
and for their internal purposes. Where we contract with third parties to
perform services, those third parties may handle your personal information and
must only use the information for the purposes for which it was supplied
although we are not be responsible for ensuring this. <br>

<br><b>Storage of information </b><br>

<span><br>We will take reasonable
steps to:<br>
- ensure that the personal information collected and used is accurate, complete
and up to date;<br>
- destroy personal information if it no longer needs it for any purpose; and<br>
- protect your personal information from misuse, loss and from unauthorised
access, modification or disclosure.</span><br>', 1, N'2012-12-10 00:00:00', NULL, NULL, 1)
INSERT INTO [dbo].[Pages] ([ID], [name], [title], [content], [added_by], [added_on], [modified_by], [modified_on], [lang]) VALUES (6, N'privacy', N'隐私条款', N'<p>隐私条款</p>', 1, N'2012-12-10 00:00:00', NULL, NULL, 4)
SET IDENTITY_INSERT [dbo].[Pages] OFF
