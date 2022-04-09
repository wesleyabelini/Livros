USE LeLivro
GO

DROP PROC IF EXISTS SIMILARITY
GO

CREATE PROCEDURE SIMILARITY @Matrix VARCHAR(MAX)
AS

EXECUTE sp_execute_external_script
@language = N'Python'
, @script = N'
from sklearn.metrics.pairwise import cosine_similarity
import numpy as np
import pandas as pd

y = (str(InputDataSet)[1:])[:-1].split(";")[1:]
a = []

for t in (((str(InputDataSet)[1:])[:-1].split(";")[0])[1:])[:-1].split(","):
	try:
		float(t)
		a.append(float(t))
	except ValueError:
		a.append(0)

a = np.asarray(a).reshape(1, -1) # representa a minha frase qual estou consultando

final = []
cont = 1

for i in y:
    i = (i[1:])[:-1].split(",")
    j = []

    for t in i:
		j.append(float(t))
	
    j = np.asarray(j).reshape(1, -1) # representa a minha de frases
    xx = cosine_similarity(j, a)
    final.append({"key": cont, "value": xx})
    cont = cont + 1

OutputDataSet = pd.DataFrame(final)',
@input_data_1 = N'SELECT @i;',
@params = N'@i VARCHAR(MAX)',
@i = @Matrix

WITH RESULT SETS
(
	(
		[key] int,
		[value] VARCHAR(5)
	)
);