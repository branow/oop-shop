# shop
|--Program.cs 
|--Controllers 
| |--Stages 
| | |--Cabinet.cs 
| | --Menu.cs 
| |--Contoleer.cs 
| |--Launcher.cs 
| --Session.cs 
| ---Logic 
|--Data 
| |--Tables 
| | |--AccountTable.cs 
| | |--CategoryTable.cs 
| | --ProductTable.cs 
| |--Condition.cs 
| |--DataBase.cs 
| |--DataField.cs 
| --Table 
|--Account.cs 
|--Operation.cs 
|--Transaction.cs 
|--Hash.cs
--Product.cs

Робота із базою даних (mysql) Всі класи що пов'язані із базою даник знаходяться у папці Data. Була спроба створити універсальний клас (Table) для роботи із базою даних, де у відповідних методах формуються відповідні команди (insert, select, delete, update) а у методи приймаю лише зміні для кожної таблиці. Для такої реалізації були створені додаткові класи DataField, Condition. DataField - приймає два значення: назву колонки і значення. Condition - був створений для передачі інформаціх типу (where column=value). Table - це абстрактний клас. Від нього варто наслідуватися щоб реалізувати вже конкретну таблицю (AccountTable, CategoryTable, ProductTable).

Взаємодія із користувачем Всі для цього необхідні класі у папці Controllers. Launcher, Controller, Session - три оснівні класи. Launcher містить метод Start де зчитується команда із консолі, передається у відповідний Controller і отримана відповідь виводиться на екран. Взаємодія Launcher і Controller відбувається за домопогою додатковаго класу Session де зберігається Respond - це поле для відповіді контролера на певну команду користувача, Account - об'єкт акаунту користувача що створюється після авторизації, Controller - це поле у який буде передана наступний контролер для Launcher, що дозволяє із одного котролера переходити до іншого. Сам класс Controller - цу абстрактний клас що викликає відповідний метод на певну команду користувача. Наслідники Controller знаходяться у папці Stages (Cabinet, Menu).