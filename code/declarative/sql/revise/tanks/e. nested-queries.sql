USE QueryRepetition
GO
SELECT TNK.[Name]
FROM Tanks AS TNK
WHERE TNK.PriceId = (

SELECT PRC.Id
FROM Prices AS PRC
WHERE PRC.Value > 5000000)


USE QueryRepetition
GO
SELECT TNK.[Name]
FROM Tanks AS TNK
WHERE TNK.GunId = (

SELECT GN.Id
FROM Guns AS GN
WHERE GN.PriceId = (

SELECT PRC.Id
FROM Prices AS PRC
WHERE PRC.[Value] = (

SELECT MIN(PRCC.Value)
FROM Prices AS PRCC)))