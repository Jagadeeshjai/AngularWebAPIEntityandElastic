# BooksandAuthors
 WEBAPI, Entity Framework, Elastic & Angular

Author
Id Integer Primary Key
Name String Required
• Use ASP.NET MVC 5 / Web API (.Net Framework)
• Using Code-First approach in Entity Framework, Create two tables i.e Book and Author
• Create a many to many relationships between Book and Author
• When application launches first time and there is no data in database, seed (insert) 20,000
records for books and each book should be tagged at least to i.e 2 authors randomly as
shown in below table
Data Tables with Seed Data
Author
Id Name
1 Author1
2 Author2
3 AuthorABC
4 AuthorAZA
Book_Author _Mapping
BookId AuthorId
1 1
1 4
… …
20000 3
• Create a listing screen of Books with Server-Side pagination. Data should be fetched via LINQ
from Entity Framework
• Paging can be only done with 2 buttons i.e Next and Previous. Please do not use any thirdparty plugins/code. You can see the listing example below
Book
Id Integer Primary Key
Name String Required
Published Boolean Required
PDFPath String Optional
Book
Id Name Published PDFPath
1 Testing Book 1 True assets/uploads/book1.pdf
2 Testing Book 2 False assets/uploads/book2.pdf
… … … …
20000 Testing Book 20000 True NULL
• In the listing screen, add a search box in which user can search by Book Title and Author.
Searching should be done (via ElasticSearch) as soon as user starts typing with a delay of
300ms. You can see the example screenshot below.
o Use AutoMapper
o Use ElasticSearch library
o Use DTO (Data Transfer Object) when you are return data to the listing screen
• Add a print button, which will print the entire grid (with no styling applied during print
mode)
• Clicking on View button in PDF Path, shall open PDF in a new browser tab
• Front-end Should be done in Angular
Good to Have
• User friend UI (Bootstrap or Custom CSS)
• Consider Best Practices when Coding if you can
• Clean Code
