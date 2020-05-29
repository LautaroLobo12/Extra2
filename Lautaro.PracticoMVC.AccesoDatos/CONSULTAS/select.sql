--UPDATE DetallesPedidos
--SET Cantidad = 3
--WHERE NumeroPedido = 1
--AND NumeroItem = 4
DELETE FROM DetallesPedidos 
WHERE NumeroPedido = 1
AND NumeroItem = 1;

SELECT * FROM DetallesPedidos
--INSERT INTO DetallesPedidos (NumeroPedido, NumeroItem, CodigoProducto, Cantidad, PrecioUnitario )
--VALUES (1, 4, 1004, 5, 152.67);

/*

SELECT column1, column2, ...
FROM table_name;

INSERT INTO table_name (column1, column2, column3, ...)
VALUES (value1, value2, value3, ...);

UPDATE table_name
SET column1 = value1, column2 = value2, ...
WHERE condition;

DELETE FROM table_name 
WHERE condition;



*/