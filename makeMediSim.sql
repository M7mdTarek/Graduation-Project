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


=================================================================================

INSERT INTO symptoms (name_ar, name_en) VALUES (N'مثير للحكة', 'itching');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'طفح جلدي', 'skin rash');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ثمرات جلدية', 'nodal skin eruptions');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'عطس مستمر', 'continuous sneezing');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ارتجاف', 'shivering');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'قشعريرة', 'chills');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم في المفاصل', 'joint pain');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم في المعدة', 'stomach pain');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'حموضة', 'acidity');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'قرح على اللسان', 'ulcers on tongue');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'فقدان العضلات', 'muscle wasting');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'قيء', 'vomiting');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'وخز أثناء التبول', 'burning micturition');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'رؤية نقاط أثناء التبول', 'spotting urination');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'التعب', 'fatigue');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'زيادة في الوزن', 'weight gain');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'القلق', 'anxiety');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'يدين وأقدام باردة', 'cold hands and feets');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تقلبات المزاج', 'mood swings');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'فقدان الوزن', 'weight loss');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'عدم الاستقرار', 'restlessness');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'كسل', 'lethargy');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'بقع في الحلق', 'patches in throat');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'مستوى سكر غير منتظم', 'irregular sugar level');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'سعال', 'cough');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'حمى عالية', 'high fever');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'عيون منخفضة', 'sunken eyes');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ضيق التنفس', 'breathlessness');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تعرق', 'sweating');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'جفاف', 'dehydration');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'اضطراب الهضم', 'indigestion');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'صداع', 'headache');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'بشرة صفراوية', 'yellowish skin');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'بول داكن', 'dark urine');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'غثيان', 'nausea');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'فقدان الشهية', 'loss of appetite');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم خلف العيون', 'pain behind the eyes');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم في الظهر', 'back pain');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'الإمساك', 'constipation');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم في البطن', 'abdominal pain');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'إسهال', 'diarrhoea');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'حمى خفيفة', 'mild fever');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'بول أصفر', 'yellow urine');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تصفير العيون', 'yellowing of eyes');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'فشل كبدي حاد', 'acute liver failure');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تحميل السوائل', 'fluid overload');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تورم في المعدة', 'swelling of stomach');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تورم في الغدد الليمفاوية', 'swelled lymph nodes');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'الإحباط', 'malaise');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'رؤية ضبابية ومشوشة', 'blurred and distorted vision');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'بلغم', 'phlegm');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تهيج في الحلق', 'throat irritation');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'احمرار العيون', 'redness of eyes');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ضغط الجيوب الأنفية', 'sinus pressure');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'سيلان الأنف', 'runny nose');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'احتقان', 'congestion');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم في الصدر', 'chest pain');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ضعف في الأطراف', 'weakness in limbs');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'سرعة نبض القلب', 'fast heart rate');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم أثناء حركات الأمعاء', 'pain during bowel movements');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم في المنطقة الشرجية', 'pain in anal region');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'براز دموي', 'bloody stool');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تهيج في الشرج', 'irritation in anus');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم في الرقبة', 'neck pain');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'دوار', 'dizziness');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تشنجات', 'cramps');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'كدمات', 'bruising');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'البدانة', 'obesity');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تورم في الساقين', 'swollen legs');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'توسع الأوعية الدموية', 'swollen blood vessels');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'وجه وعيون منتفخة', 'puffy face and eyes');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تضخم الغدة الدرقية', 'enlarged thyroid');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'أظافر هشة', 'brittle nails');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تورم في الأطراف', 'swollen extremeties');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'جوع مفرط', 'excessive hunger');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'علاقات زوجية خارج الزواج', 'extra marital contacts');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'جفاف ووخز في الشفاه', 'drying and tingling lips');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'كلام غير واضح', 'slurred speech');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم في الركبة', 'knee pain');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم في مفصل الورك', 'hip joint pain');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ضعف في العضلات', 'muscle weakness');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'رقبة صلبة', 'stiff neck');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تورم في المفاصل', 'swelling joints');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تصلب الحركة', 'movement stiffness');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'حركات دائرية', 'spinning movements');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'فقدان التوازن', 'loss of balance');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'عدم استقرار', 'unsteadiness');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ضعف في جهة الجسم الواحدة', 'weakness of one body side');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'فقدان الشم', 'loss of smell');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'عدم راحة في المثانة', 'bladder discomfort');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'رائحة كريهة للبول', 'foul smell of urine');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'شعور مستمر بالبول', 'continuous feel of urine');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'مرور الغازات', 'passage of gases');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'حكة داخلية', 'internal itching');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'مظهر سام (تيفوس)', 'toxic look (typhos)');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'الاكتئاب', 'depression');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'التهيج', 'irritability');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم في العضلات', 'muscle pain');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تغير في الوعي', 'altered sensorium');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'بقع حمراء على الجسم', 'red spots over body');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ألم في البطن', 'belly pain');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'دورة شهرية غير طبيعية', 'abnormal menstruation');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تغير لون الجلد', 'dischromic patches');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تسيل العيون', 'watering from eyes');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'زيادة في الشهية', 'increased appetite');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'زيادة في إفراز البول', 'polyuria');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تاريخ عائلي', 'family history');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'بلغم مخاطي', 'mucoid sputum');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'بلغم صدئ', 'rusty sputum');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'نقص التركيز', 'lack of concentration');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'اضطرابات بصرية', 'visual disturbances');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'استلام نقل دم', 'receiving blood transfusion');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تلقي حقن غير معقمة', 'receiving unsterile injections');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'غيبوبة', 'coma');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'نزيف في المعدة', 'stomach bleeding');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'توسع في البطن', 'distention of abdomen');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تاريخ استهلاك الكحول', 'history of alcohol consumption');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'دم في البلغم', 'blood in sputum');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'الأوردة الممتازة على الساق', 'prominent veins on calf');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'خفقان القلب', 'palpitations');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'المشي المؤلم', 'painful walking');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'بثور مملوءة بالصديد', 'pus filled pimples');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'نقاط سوداء', 'blackheads');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تقشير', 'scurring');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'تقشير الجلد', 'skin peeling');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'غبار فضي مثل الجلد', 'silver like dusting');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'ثقوب صغيرة في الأظافر', 'small dents in nails');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'الأظافر التي تعاني من التهاب', 'inflammatory nails');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'فقاعة', 'blister');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'قرحة حمراء حول الأنف', 'red sore around nose');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'صفيحة صفراء تنزف', 'yellow crust_ooze');
INSERT INTO symptoms (name_ar, name_en) VALUES (N'التشخيص', 'prognosis');
