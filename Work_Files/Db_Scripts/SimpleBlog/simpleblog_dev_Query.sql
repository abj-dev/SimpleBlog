CREATE SCHEMA `simpleblog_db` DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci ;

INSERT INTO role_users (user_id, role_id) VALUES (1,1);
INSERT INTO role_users (user_id, role_id) VALUES (2,2);
INSERT INTO role_users (user_id, role_id) VALUES (1,3);
INSERT INTO role_users(user_id, role_id) VALUES (2, 1);

UPDATE roles SET name = 'visitor' WHERE id = 2;

INSERT INTO `simpleblog_db`.`posts` (`user_id`, `title`, `slug`, `created_at`, `content`) VALUES ('2', 'Nexus Review', 'Nexus Review', '2015-04-27 22:15:35', 'The nexus 6 is the top of the line new smart phone.');
INSERT INTO `simpleblog_db`.`posts` (`user_id`, `title`, `slug`, `created_at`, `content`) VALUES ('1', 'Samsung Review', 'Samsung Review', '2015-04-27 22:15:35', 'The samsung galaxy is the top of the line smart phone.');

UPDATE simpleblog_db.posts SET created_at = now();

INSERT INTO `simpleblog_db`.`users` (`Username`, `email`, `password_hash`) VALUES ('nelson', 'nelson@simpleblog.com', '$2a$10$7.crXmuImvwDUIhNF31Oq.YEmswFhsN4Fa2dUOhrchoNOziw6eBKW');
INSERT INTO `simpleblog_db`.`users` (`Username`, `email`, `password_hash`) VALUES ('steve', 'steve@simpleblog.com', '$2a$10$nPm4aHYXJy42pi2.wzbQZuBb4nJY2YUvXUqAK85CeLVjYpGXFFTYW');

INSERT INTO `simpleblog_db`.`roles` (`name`) VALUES ('admin');
INSERT INTO `simpleblog_db`.`roles` (`name`) VALUES ('visitor');
INSERT INTO `simpleblog_db`.`roles` (`name`) VALUES ('blogger');

INSERT INTO `simpleblog_db`.`tags` (`slug`, `name`) VALUES ('programming', 'Programming');
INSERT INTO `simpleblog_db`.`tags` (`slug`, `name`) VALUES ('design', 'Design');
INSERT INTO `simpleblog_db`.`tags` (`slug`, `name`) VALUES ('awesome', 'Awesome');

INSERT INTO `simpleblog_db`.`post_tags` (`post_id`, `tag_id`) VALUES ('4', '2');
INSERT INTO `simpleblog_db`.`post_tags` (`post_id`, `tag_id`) VALUES ('3', '1');
INSERT INTO `simpleblog_db`.`post_tags` (`post_id`, `tag_id`) VALUES ('2', '3');