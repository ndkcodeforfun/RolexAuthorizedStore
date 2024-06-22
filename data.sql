use RolexAuthorizedStoreDb
GO

-- Add categories
INSERT INTO Categories (Name, Description, Status)
VALUES
  ('Day-Date', 'A classic and elegant watch for everyday wear.', 1),
  ('Submariner', 'A robust and reliable diving watch.', 1),
  ('GMT-Master II', 'A sophisticated travel watch with dual time zone function.', 1),
  ('Cosmograph Daytona', 'A high-performance chronograph for racing enthusiasts.', 1),
  ('Explorer', 'A durable and functional watch for exploration.', 1);

-- Add products
-- Day-Date
INSERT INTO Products (CategoryId, Name, Description, Price, Status)
VALUES
  (1, 'Rolex Day-Date 40', '18 ct yellow gold - President bracelet - Champagne-colour dial - Diamond-set hour markers', 42050, 1);
INSERT INTO Products (CategoryId, Name, Description, Price, Status)
VALUES
  (1, 'Rolex Day-Date 36', '18 ct white gold - President bracelet - White dial - Diamond-set hour markers', 38650, 1);

  -- Submariner
INSERT INTO Products (CategoryId, Name, Description, Price, Status)
VALUES
  (2, 'Rolex Submariner', 'Oystersteel - Oyster bracelet - Black dial - Cerachrom bezel', 9100, 1);
INSERT INTO Products (CategoryId, Name, Description, Price, Status)
VALUES
  (2, 'Rolex Submariner Date', 'Oystersteel and yellow gold - Oyster bracelet - Black dial - Cerachrom bezel', 14600, 1);

  -- GMT-Master II
INSERT INTO Products (CategoryId, Name, Description, Price, Status)
VALUES
  (3, 'Rolex GMT-Master II', 'Oystersteel and Everose gold - Jubilee bracelet - Black dial - Cerachrom bezel', 15900, 1);
  INSERT INTO Products (CategoryId, Name, Description, Price, Status)
VALUES
  (3, 'Rolex GMT-Master II', 'White gold - Oyster bracelet - Meteorite dial - Cerachrom bezel', 41150, 1);

  -- Cosmograph Daytona
INSERT INTO Products (CategoryId, Name, Description, Price, Status)
VALUES
  (4, 'Rolex Cosmograph Daytona', 'Oystersteel - Oyster bracelet - White dial - Black Cerachrom bezel', 14100, 1);
  INSERT INTO Products (CategoryId, Name, Description, Price, Status)
VALUES
  (4, 'Rolex Cosmograph Daytona', '18 ct yellow gold - Oysterflex bracelet - Black and yellow gold dial - Black Cerachrom bezel', 30200, 1);

  -- Explorer
INSERT INTO Products (CategoryId, Name, Description, Price, Status)
VALUES
  (5, 'Rolex Explorer', 'Oystersteel - Oyster bracelet - Black dial', 7350, 1);
  INSERT INTO Products (CategoryId, Name, Description, Price, Status)
VALUES
  (5, 'Rolex Explorer II', 'Oystersteel - Oyster bracelet - White dial', 8600, 1);