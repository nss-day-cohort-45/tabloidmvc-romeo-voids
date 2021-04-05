      SELECT c.Id, c.PostId, c.UserProfileId, 
                              c.Subject, c.Content,
                              c.CreateDateTime, 
                              p.Title AS PostTitle,
                              u.DisplayName
                         FROM Comment c
                              LEFT JOIN Post p ON c.PostId = p.id
                              LEFT JOIN UserProfile u ON p.UserProfileId = u.id


SET IDENTITY_INSERT [Comment] ON
INSERT INTO Comment (Id, PostId, UserProfileId, [Subject], Content, CreateDateTime)
VALUES (1, 1, 1, 'Post 1', 'Comment Test', '4/2/2021')
SET IDENTITY_INSERT [Comment] OFF

SELECT * FROM Comment