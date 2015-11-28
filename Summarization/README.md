#Automatic Summarization

We are doing Job Desription summarization in this module. There are two types of Auto Summarization

1. Extraction based summarization 
2. Abstract based Summarization

We are using Extraction based summarization.

Below are the steps for the Extraction based summarization

1. Sentence splitting
2. Preprocessing (stop word removal).
3. Tokenization.
4. Job Description summarization
		We are usign Sentence ranking for summerize the description. Sentence ranking is done using the below features.
		
			1. TD-IDF Sum: The Sum of TD-IDF sum of all the words in a sentence.
			2. Sentence Length
			3. Important words
			4. Upper Case letters Count
			5. Nouns Count
			6. Verbs Count
			7. Adjective count
			8. Number of the sentence.
			
		The idea is to build a classifier by annotating the senteces in the job description manually and training a model. But as this need some time this is left for the future work.
		Currently we are using weights for the above features and using the sum of the weights as the score of the sentence.
5. Extraction of Must and Good to have details from the JOb description.
		We will parse the sentence using the dependecy parser and below are the rules for extraction the Must haves:
			Take the VBD 
		


