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

������ �� ����� ����� (mysql) �� ����� �� ���'���� �� ����� ����� ����������� � ����� Data. ���� ������ �������� ������������ ���� (Table) ��� ������ �� ����� �����, �� � ��������� ������� ���������� ������� ������� (insert, select, delete, update) � � ������ ������� ���� ��� ��� ����� �������. ��� ���� ��������� ���� ������� �������� ����� DataField, Condition. DataField - ������ ��� ��������: ����� ������� � ��������. Condition - ��� ��������� ��� �������� ���������� ���� (where column=value). Table - �� ����������� ����. ³� ����� ����� ������������ ��� ���������� ��� ��������� ������� (AccountTable, CategoryTable, ProductTable).

������� �� ������������ �� ��� ����� �������� ���� � ����� Controllers. Launcher, Controller, Session - ��� ����� �����. Launcher ������ ����� Start �� ��������� ������� �� ������, ���������� � ��������� Controller � �������� ������� ���������� �� �����. ������� Launcher � Controller ���������� �� ��������� ����������� ����� Session �� ���������� Respond - �� ���� ��� ������ ���������� �� ����� ������� �����������, Account - ��'��� ������� ����������� �� ����������� ���� �����������, Controller - �� ���� � ���� ���� �������� ��������� ��������� ��� Launcher, �� �������� �� ������ ��������� ���������� �� ������. ��� ����� Controller - �� ����������� ���� �� ������� ��������� ����� �� ����� ������� �����������. ��������� Controller ����������� � ����� Stages (Cabinet, Menu).