create database MediSim
use MediSim

CREATE TABLE users (
    id INT PRIMARY KEY IDENTITY,
    username NVARCHAR(255) NOT NULL,
    email NVARCHAR(255) NOT NULL,
    password NVARCHAR(255) NOT NULL, -- assuming password is stored encrypted
    height FLOAT,
    weight FLOAT,
    age INT,
    ismale BIT
);

CREATE TABLE admins (
    id INT PRIMARY KEY, -- Manually set, as specified
    email NVARCHAR(255) NOT NULL,
    password NVARCHAR(255) NOT NULL
);

CREATE TABLE chronic_diseases (
    id INT PRIMARY KEY IDENTITY,
    name_en NVARCHAR(255),
    name_ar NVARCHAR(255) -- Arabic version of the name
);

CREATE TABLE user_chronic_diseases (
    user_id INT,
    disease_id INT,
    PRIMARY KEY (user_id, disease_id),
    FOREIGN KEY (user_id) REFERENCES users(id),
    FOREIGN KEY (disease_id) REFERENCES chronic_diseases(id)
);

CREATE TABLE posts (
    id INT PRIMARY KEY IDENTITY,
    disease_id INT,  -- Foreign key to chronic_disease
    title_ar NVARCHAR(255),  -- Arabic title
    title_en NVARCHAR(255),  -- English title
    description_ar NVARCHAR(MAX),  -- Arabic description
    description_en NVARCHAR(MAX),  -- English description
    image NVARCHAR(255),  -- Assuming storing image path or URL
    FOREIGN KEY (disease_id) REFERENCES chronic_diseases(id)
);

CREATE TABLE symptoms (
    id INT PRIMARY KEY IDENTITY,
    name_ar NVARCHAR(255), -- Arabic name
    name_en NVARCHAR(255) -- English name
);

CREATE TABLE test (
    id INT PRIMARY KEY IDENTITY,
    name_ar NVARCHAR(255), -- Arabic name
    name_en NVARCHAR(255) -- English name
);

CREATE TABLE drugs (
    id INT PRIMARY KEY IDENTITY,
    name_ar NVARCHAR(255), -- Arabic name
    name_en NVARCHAR(255), -- English name
    scientific_name_ar NVARCHAR(255), -- Arabic scientific name
    scientific_name_en NVARCHAR(255), -- English scientific name
    classification_ar NVARCHAR(255), -- Arabic classification
    classification_en NVARCHAR(255), -- English classification
    category_ar NVARCHAR(255), -- Arabic category
    category_en NVARCHAR(255), -- English category
    description_ar NVARCHAR(MAX), -- Arabic description
    description_en NVARCHAR(MAX), -- English description
    image NVARCHAR(255) -- Assuming storing image path or URL
);

CREATE TABLE email_otp (
    email NVARCHAR(255) NOT NULL,
    otp NVARCHAR(6) NOT NULL, -- Assuming OTP is a 6-character string
    CONSTRAINT PK_email_otp PRIMARY KEY (email)
);