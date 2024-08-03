# MediSim API

## Project Overview :

MediSim API is a RESTful API designed to mobile application 
that aims to assist patients in predicting the likelihood of various diseases, including user registration, authentication. It incorporates essential features like  dependency injection, JWT token-based authentication, and multiple controllers for handling different aspects of the project.


## Picture of API :

![Screenshot 2024-08-03 181413](https://github.com/user-attachments/assets/19a7672f-6df8-4249-8e7c-b39adabae904)



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

The project offers the following API endpoints for various functionalities:

### Authentication

#### POST `/Authentication/signup`
- **Description**: Registers a new user with the provided details.
- **Request Parameters**:
  - `SignupDto request`: A data transfer object containing the user's registration details.
    - `string userName`: The username of the user.
    - `string email`: The email address of the user.
    - `int age`: The age of the user.
    - `double height`: The height of the user in meters.
    - `double weight`: The weight of the user in kilograms.
    - `string password`: The user's password.
    - `bool isMale`: The gender of the user, where `true` indicates male and `false` indicates female.
    - `List<int> selectedChronic`: A list of chronic disease IDs associated with the user.
- **Response**:
  - **200 OK**: If the registration is successful, returns a JSON object with a token.
    ```json
    {
      "token": "string"
    }
    ```
  - **400 Bad Request**: If there are any validation errors, returns an error message.
    - "invalid email": If the email format is invalid.
    - "the email is already used": If the email is already registered.
    - "invalid Height": If the height is invalid.
    - "invalid Weight": If the weight is invalid.
    - "invalid Age": If the age is invalid.

#### POST `/Authentication/login`
- **Description**: Authenticates a user and returns a token if the login is successful.
- **Request Parameters**:
  - `LoginDto request`: A data transfer object containing the user's login details.
    - `string email`: The email address of the user.
    - `string password`: The user's password.
- **Response**:
  - **200 OK**: If the login is successful, returns a JSON object with a token and the username.
    ```json
    {
      "token": "string",
      "userName": "string"
    }
    ```
  - **400 Bad Request**: If there are any validation errors, returns an error message.
    - "the email is wrong": If the email is not found.
    - "the password is wrong": If the password is incorrect.

#### POST `/Authentication/forgetpassword`
- **Description**: Initiates the password reset process by sending a one-time password (OTP) to the user's email.
- **Request Parameters**:
  - `MailDto request`: A data transfer object containing the user's email address.
    - `string mail`: The email address of the user.
- **Response**:
  - **200 OK**: If the email is found and the OTP is sent successfully, returns a success message.
    ```json
    {
      "message": "the mail has been sent successfully"
    }
    ```
  - **400 Bad Request**: If the email is not found, returns an error message.
    - "the email is not found": If the email is not registered in the system.

#### POST `/Authentication/verifyotp`
- **Description**: Verifies the one-time password (OTP) sent to the user's email during the password reset process.
- **Request Parameters**:
  - `MailDto request`: A data transfer object containing the user's email address and the OTP.
    - `string mail`: The email address of the user.
    - `string otp`: The one-time password sent to the user's email.
- **Response**:
  - **200 OK**: If the OTP is valid, returns a success message.
    ```json
    {
      "message": "the OTP is valid"
    }
    ```
  - **400 Bad Request**: If the OTP is invalid, returns an error message.
    - "the OTP is invalid": If the provided OTP is incorrect.

#### POST `/Authentication/changepassword`
- **Description**: Changes the user's password after verifying the OTP and ensuring the new passwords match.
- **Request Parameters**:
  - `MailDto request`: A data transfer object containing the user's email address and new password details.
    - `string mail`: The email address of the user.
    - `string password`: The new password for the user.
    - `string confirmpassword`: The confirmation of the new password.
- **Response**:
  - **200 OK**: If the password is successfully updated, returns a success message.
    ```json
    {
      "message": "the password updated successfully"
    }
    ```
  - **400 Bad Request**: If there are any validation errors, returns an error message.
    - "the two passwords don't match": If the `password` and `confirmpassword` do not match.

### FetchData

#### GET `/FetchData/getchronicdisease`
- **Description**: Retrieves a list of all chronic diseases.
- **Request Parameters**: None
- **Response**:
  - **200 OK**: Returns a JSON array of chronic diseases.
    ```json
    [
      {
        "id": 1,
        "name_en": "Hypertension",
        "name_ar": "ارتفاع ضغط الدم"
      },
      {
        "id": 2,
        "name_en": "Diabetes",
        "name_ar": "السكري"
      }
      // More chronic diseases...
    ]
    ```

#### POST `/FetchData/searchfordrug`
- **Description**: Searches for a drug by its name.
- **Request Parameters**:
  - `string searchTerm`: The name of the drug to search for.
- **Response**:
  - **200 OK**: If the drug is found, returns a JSON object with the drug details.
    ```json
    {
      "id": 1,
      "name_ar": "باراسيتامول",
      "name_en": "Paracetamol",
      "scientific_name_ar": "باراسيتامول",
      "scientific_name_en": "Paracetamol",
      "classification_ar": "مسكنات",
      "classification_en": "Analgesics",
      "category_ar": "OTC",
      "category_en": "OTC",
      "description_ar": "يستخدم لتخفيف الألم والحمى.",
      "description_en": "Used to relieve pain and fever.",
      "image": "url/to/image.jpg"
    }
    ```
  - **400 Bad Request**: If the search term is empty, returns an error message.
    - "the string is empty": If `searchTerm` is null or empty.
  - **404 Not Found**: If the drug is not found, returns an error message.
    - "the drug not found": If the drug is not found in the database.

#### GET `/FetchData/getsymptoms`
- **Description**: Retrieves a list of all symptoms.
- **Request Parameters**: None
- **Response**:
  - **200 OK**: Returns a JSON array of symptoms.
    ```json
    [
      {
        "id": 1,
        "name_ar": "الصداع",
        "name_en": "Headache"
      },
      {
        "id": 2,
        "name_ar": "الحمى",
        "name_en": "Fever"
      }
      // More symptoms...
    ]
    ```

#### GET `/FetchData/getposts`
- **Description**: Retrieves all posts and indicates whether each post is relevant (advice) based on the user's chronic diseases.
- **Request Parameters**: None (requires user authentication via a Bearer token)
- **Response**:
  - **200 OK**: Returns a JSON array of posts. Each post includes a flag `isAdvice` indicating whether it is relevant to the user's chronic diseases.
    ```json
    [
      {
        "id": 1,
        "disease_id": 2,
        "title_ar": "نصائح لمرض السكري",
        "title_en": "Diabetes Tips",
        "description_ar": "وصف نصائح لمرض السكري.",
        "description_en": "Description of diabetes tips.",
        "image": "url/to/image.jpg",
        "isAdvice": true
      },
      {
        "id": 2,
        "disease_id": 1,
        "title_ar": "نصائح لارتفاع ضغط الدم",
        "title_en": "Hypertension Tips",
        "description_ar": "وصف نصائح لارتفاع ضغط الدم.",
        "description_en": "Description of hypertension tips.",
        "image": "url/to/image.jpg",
        "isAdvice": false
      }
      // More posts...
    ]
    ```

### Prediction

#### POST `/Prediction/predictdisease`
- **Description**: Predicts the disease based on the provided symptoms. 
The symptoms are processed and sent to an external API for disease prediction.
- **Request Parameters**:
  - `List<int> symptomsIds`: A list of symptom IDs that represent the symptoms experienced by the user.
- **Response**:
  - **200 OK**: If the prediction is successful, returns a JSON array of disease prediction results.
    ```json
    [
      {
        "enDiseaseName": "Diabetes",
        "arDiseaseName": "السكري",
        "confidence": 0.85,
        "enDiseaseDescription": "A chronic condition that affects the way the body processes blood sugar (glucose).",
        "arDiseaseDescription": "حالة مزمنة تؤثر على طريقة معالجة الجسم للسكر في الدم.",
        "enAdvices": "Monitor blood sugar levels regularly.",
        "arAdvices": "راقب مستويات السكر في الدم بانتظام."
      }
      // More disease predictions...
    ]
    ```
  - **404 Not Found**: If the prediction request fails or no result is returned, returns an error message.
    - "the modelbit request failed": If the external API request fails or returns no result.



#### POST `/Prediction/predictskindisease`
- **Description**: Predicts skin diseases based on an uploaded image. The image is converted to a base64 string and sent to an external API for skin disease prediction.
- **Request Parameters**:
  - `IFormFile image`: An image file uploaded by the user for skin disease prediction.
- **Response**:
  - **200 OK**: If the prediction is successful, returns a JSON object with the disease prediction results.
    ```json
    {
      "enDiseaseName": "Melanoma",
      "arDiseaseName": "الميلانوما",
      "enDiseaseDescription": "A type of skin cancer that can spread to other parts of the body.",
      "arDiseaseDescription": "نوع من السرطان الجلد الذي يمكن أن ينتشر إلى أجزاء أخرى من الجسم."
    }
    ```
  - **400 Bad Request**: If no file is uploaded or the file is empty, returns an error message.
    - "No file uploaded.": If the `image` parameter is null or its length is zero.
  - **404 Not Found**: If the prediction request fails or no result is returned, returns an error message.
    - "the modelbit request failed": If the external API request fails or returns no result.


### Frontend Project

This backend is designed to work with the corresponding frontend application. You can find the frontend GitHub repository at the following link:

- [mobile application GitHub Repository](https://github.com/Mahmoudadel17/Graduation-Project)

#### Mobile App Developer

The  mobile application were developed by: **[Mahmoud Adel](https://github.com/Mahmoudadel17)** 




