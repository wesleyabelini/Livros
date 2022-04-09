USE LeLivro
GO

DROP PROC IF EXISTS GETMATRIX
GO

CREATE PROCEDURE GETMATRIX @MYTEXT VARCHAR(MAX)
AS

DECLARE @STOPWORDS NVARCHAR(MAX) = (SELECT * FROM StopWords FOR JSON AUTO)
DECLARE @DICIONARIO NVARCHAR(MAX) = (SELECT * FROM Dicionario FOR JSON AUTO)

EXECUTE sp_execute_external_script  
@language = N'Python'
, @script = N'
import pandas as pd
import numpy as np
import heapq
import math
import json
import unidecode
import spacy
import sys

np.set_printoptions(threshold=sys.maxsize)

frasecheck = str(InputDataSet)
stopwords = pd.read_json(stopW)
dicioptbr = pd.read_json(dicioN)

frasecheck = " ".join([unidecode.unidecode(word) for word in frasecheck.split() if word.lower() not in stopwords and word.isalpha()])

with open("C:\\Users\\w_vic\\pythonfiles\\word2count2.json", encoding="utf-8") as fh:
    word2count = json.load(fh)

nlp = spacy.load("C:\Program Files\Python39\Lib\site-packages\pt_core_news_sm\pt_core_news_sm-2.2.0")
    
freq_words = heapq.nlargest(53624,word2count, key=word2count.get)

docFrase = nlp(frasecheck)
for token in docFrase:
	if token.lemma_ in dicioptbr: 
		frasecheck = frasecheck.replace(str(token), token.lemma_)

frasecheck = " ".join([unidecode.unidecode(word) for word in frasecheck.split() if word.lower() not in stopwords and word.isalpha()])

z = []

vector = []
for word in freq_words:
	tf = frasecheck.count(word) / len(frasecheck.split(" "))
	idf = math.log(11682 / word2count[word])
	tfidf = tf * idf
	tfidf = ("{:.3f}".format(tfidf)).replace("0.000", "0")
	vector.append(tfidf)

z.append(vector)

a = np.asarray(z)
data =	{ "mx":	[str(a.reshape(1, -1))] }

OutputDataSet = pd.DataFrame(data)
',
@input_data_1 = N'SELECT @i;',
@params = N'@i VARCHAR(MAX), @stopW VARCHAR(MAX), @dicioN VARCHAR(MAX)',
@i = @MYTEXT,
@stopW = @STOPWORDS,
@dicioN = @DICIONARIO
WITH RESULT SETS(([Matrix] VARCHAR(MAX) NOT NULL));