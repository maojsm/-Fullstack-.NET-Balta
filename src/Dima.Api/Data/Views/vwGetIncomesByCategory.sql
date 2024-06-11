-- View para postgreSQL deve ser digitada no PgAdmin4

--CREATE OR REPLACE VIEW "vwGetIncomesByCategory" AS
--    SELECT
--        "Transaction"."UserId",
--        "Category"."Title" AS "Category",
--        EXTRACT(YEAR FROM "Transaction"."PaidOrReceivedAt") AS "Year",
--        SUM("Transaction"."Amount") AS "Incomes"
--    FROM
--        "Transaction"
--        INNER JOIN "Category" ON "Transaction"."CategoryId" = "Category"."Id"
--    WHERE
--        "Transaction"."PaidOrReceivedAt" >= (current_date - INTERVAL '11 MONTH')
--        AND "Transaction"."PaidOrReceivedAt" < (current_date + INTERVAL '1 MONTH')
--        AND "Transaction"."Type" = 1
--    GROUP BY
--        "Transaction"."UserId",
--        "Category"."Title",
--        EXTRACT(YEAR FROM "Transaction"."PaidOrReceivedAt");



--CREATE OR ALTER VIEW [vwGetIncomesByCategory] AS
--    SELECT
--        [Transaction].[UserId],
--        [Category].[Title] AS [Category],
--        YEAR([Transaction].[PaidOrReceivedAt]) AS [Year],
--        SUM([Transaction].[Amount]) AS [Incomes]
--    FROM
--        [Transaction]
--            INNER JOIN [Category]
--                       ON [Transaction].[CategoryId] = [Category].[Id]
--    WHERE
--        [Transaction].[PaidOrReceivedAt]
--            >= DATEADD(MONTH, -11, CAST(GETDATE() AS DATE))
--      AND [Transaction].[PaidOrReceivedAt]
--        < DATEADD(MONTH, 1, CAST(GETDATE() AS DATE))
--      AND [Transaction].[Type] = 1
--    GROUP BY
--        [Transaction].[UserId],
--        [Category].[Title],
--        YEAR([Transaction].[PaidOrReceivedAt])