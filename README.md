# MediSim API

## Project Overview :

MediSim API is a RESTful API designed to mobile application 
that aims to assist patients in predicting the likelihood of various diseases, including user registration, authentication. It incorporates essential features like  dependency injection, JWT token-based authentication, and multiple controllers for handling different aspects of the project.


## Picture of API :

![Screenshot 2024-08-03 181413](https://github.com/user-attachments/assets/617b995e-554d-4e0d-a5e5-ac543a0cbd4c)


## Features :

### 1) *Authentication* : 

Registration and login functionalities, allowing users to  create accounts and obtain JWT tokens for authenticated access. allow users to change password with otp via mail.

### 2) *Disease Prediction* : 

Helps patients predict the percentages of the diseases they might suffer from based on their symptoms.

### 3) *Skin Disease Prediction* : 

Enables patients to input images of their skin for AI-based diagnosis.

### 4) *Chronic Disease Management* : 

Provides tips for individuals with chronic diseases and retains patient data to monitor their conditions.


### 5) *Medication Information* : 

Allows users to search for medications, displaying their uses, contraindications, precautions, doses, 
and methods of use.
   
## API Endpoints :

#### Authentication
-  POST `/Authentication/signup` :
| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `api_key` | `string` | **Required**. Your API key |

-  POST `/Authentication/login` :
-  POST `/Authentication/forgetpassword` :
-  POST `/Authentication/verifyotp` :
-  POST `/Authentication/changepassword` :
#### FetchData
-  GET `/FetchData/getchronicdisease` :
-  POST `/FetchData/searchfordrug` :
-  GET `/FetchData/getsymptoms` :
-  GET `/FetchData/getposts` :
#### Prediction
-  POST `/Prediction/predictdisease` :
-  POST `/Prediction/predictskindisease` :





