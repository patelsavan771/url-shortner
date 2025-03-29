create schema `url-shortener` ;

CREATE TABLE `url_shortener`.`url_master` (
  `id` int NOT NULL AUTO_INCREMENT,
  `code` VARCHAR(6) NULL,
  `url` VARCHAR(125) NULL,
  `createdOn` DATETIME NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `code_UNIQUE` (`code` ASC) VISIBLE,
  key `idx_url_master_code` (code));