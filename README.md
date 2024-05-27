# Lab-7
Тема: реализация метода рекурсивного спуска для синтаксического анализа.<br />

Цель работы: разработать для грамматики алгоритм синтаксического анализа на основе метода рекурсивного спуска.<br />

Для грамматики G[expression] разработать и реализовать алгоритм анализа на основе метода рекурсивного спуска. <br />
G[expression]: <br />
1. expression := multExpression | powExpression | expression ('+'|'-') multExpression | expression ('+'|'-') powExpression <br />
2. multExpression := ID | multExpression ('*'|'/') ID <br />
3. powExpression := ID '^' powExpression | ID <br />
ID – идентификатор Б{Б|Ц}, Б – {a, b, c, ...z, A, B, …, Z}, Ц – {0, 1, …, 9}<br />

![image](https://github.com/Grayvendor/Lab-7/assets/160223599/3089fef2-3361-45bf-bdf9-6a9a4f363a66)<br />
![image](https://github.com/Grayvendor/Lab-7/assets/160223599/db5fad6b-09aa-4bd3-ae14-44930ca2711e)<br />
