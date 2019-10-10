
--CREATE Database simpleblog_db;

INSERT INTO users (Username, email, password_hash) VALUES ('nelson', 'nelson@simpleblog.com', '$2a$10$7.crXmuImvwDUIhNF31Oq.YEmswFhsN4Fa2dUOhrchoNOziw6eBKW');
INSERT INTO users (Username, email, password_hash) VALUES ('steve', 'steve@simpleblog.com', '$2a$10$nPm4aHYXJy42pi2.wzbQZuBb4nJY2YUvXUqAK85CeLVjYpGXFFTYW');
	    
INSERT INTO roles (name) VALUES ('admin');
INSERT INTO roles (name) VALUES ('visitor');
INSERT INTO roles (name) VALUES ('blogger');

UPDATE roles SET name = 'visitor' WHERE id = 2;

INSERT INTO posts (user_id, title, slug, created_at, content) VALUES ('4', 'Nexus Review', 'Nexus Review', '2015-04-27 22:15:35', 'The nexus 6 is the top of the line new smart phone.');
INSERT INTO posts (user_id, title, slug, created_at, content) VALUES ('3', 'Samsung Review', 'Samsung Review', '2015-04-27 22:15:35', 'The samsung galaxy is the top of the line smart phone.');
INSERT INTO posts (user_id, title, slug, created_at, content) VALUES ('2', 'Octopus Review', 'Octopus Review', '2015-04-27 22:15:35', 'The nexus 6 is the top of the line new smart phone.');
INSERT INTO posts (user_id, title, slug, created_at, content) VALUES ('1', 'TeamCity Review', 'TeamCity Review', '2015-04-27 22:15:35', 'The samsung galaxy is the top of the line smart phone.');

UPDATE posts SET created_at = GetDate();
		    
INSERT INTO tags (slug, name) VALUES ('programming', 'Programming');
INSERT INTO tags (slug, name) VALUES ('design', 'Design');
INSERT INTO tags (slug, name) VALUES ('awesome', 'Awesome');
		    
INSERT INTO post_tags (post_id, tag_id) VALUES ('4', '2');
INSERT INTO post_tags (post_id, tag_id) VALUES ('3', '1');
INSERT INTO post_tags (post_id, tag_id) VALUES ('2', '3');

INSERT INTO role_users (user_id, role_id) VALUES (1,1);
INSERT INTO role_users (user_id, role_id) VALUES (2,2);
INSERT INTO role_users (user_id, role_id) VALUES (1,3);
INSERT INTO role_users(user_id, role_id) VALUES (2, 1);