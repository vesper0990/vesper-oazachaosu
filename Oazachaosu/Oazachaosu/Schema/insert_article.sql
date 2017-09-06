INSERT INTO Category (Name, Url) VALUES (
'C#',
'c_sharp'
);

INSERT INTO Category (Name, Url) VALUES (
'Java',
'java'
);

INSERT INTO Category (Name, Url) VALUES (
'Wzorce projektowe',
'design_pattern'
);

INSERT INTO Category (Name, Url) VALUES (
'WPF',
'wpf'
);

INSERT INTO Category (Name, Url) VALUES (
'MVVM',
'mvvm'
);


INSERT INTO Article (Title, Abstract, ContentUrl, DateTime) VALUES (
'Wzorce projektowe - Strategia',
 'Tworzymy rodziny algorytmów - wydzielamy powiązane ze sobą procesy do oddzielnych klas, redukujemy instrukcje warunkowe, polepszając jakościowo kod, a w konsekwencji tworzymy możliwość wielokrotnego jego wykorzystania.',
 'design_pattern_strategy',
 '2016-01-01T00:00:00');

INSERT INTO ArticleCategory (ArticleId, CategoryId) VALUES (
1,
2
);

INSERT INTO ArticleCategory (ArticleId, CategoryId) VALUES (
1,
3
);

INSERT INTO Article (Title, Abstract, ContentUrl, DateTime) VALUES (
'Wzorce projektowe - Obserwator',
 'W jaki sposób przekazać informację o zmianie stanu naszego obiektu innym? Wzorzec obserwator w bardzo elegancki sposób daje nam taką możliwość. Teraz nie będziemy mieli problemu z przekazywaniem informacji pomiędzy różnymi częsciami naszego projektu.',
 'design_pattern_observer', 
'2016-01-08T00:00:00');

INSERT INTO ArticleCategory (ArticleId, CategoryId) VALUES (
2,
2
);

INSERT INTO ArticleCategory (ArticleId, CategoryId) VALUES (
2,
3
);

INSERT INTO Article (Title, Abstract, ContentUrl, DateTime) VALUES (
'Wzorce projektowe - Prosta fabryka',
 'Metoda pozwalająca na tworzenie obiektów bez bezpośredniego pokazywania kodu tworznie klientowi. Prosta technika, która zabezpieczy nas przed powtarzniem kodu tworzenia obiektu należącego do pewnej rodziny.',
 'design_pattern_simple_factory',
 '2016-01-15T00:00:00');


INSERT INTO ArticleCategory (ArticleId, CategoryId) VALUES (
3,
2
);

INSERT INTO ArticleCategory (ArticleId, CategoryId) VALUES (
3,
3
);

INSERT INTO Article (Title, Abstract, ContentUrl, DateTime) VALUES (
'Wzorce projektowe - Singleton',
'Czasami pojawia sie taka sytuacja, że chcemy uzyskać tylko jedną instancję konkretnej klasy. Dla przykładu może to być klasa zajmująca się ustawieniami naszej aplikacji, bądź menadżer okienek. Dzięki takiej klasie możemy enkapsulować zarządzanie wewnętrznymi oraz zewnętrznymi zasobami i uzyskać do nich globalny dostęp.',
'design_pattern_singleton',
'2016-04-11T00:00:00');


INSERT INTO ArticleCategory (ArticleId, CategoryId) VALUES (
4,
2
);

INSERT INTO ArticleCategory (ArticleId, CategoryId) VALUES (
4,
3
);

INSERT INTO Article (Title, Abstract, ContentUrl, DateTime) VALUES (
'Wzorzec MVVM - wprowadzenie',
'Szybki rozwój aplikacji skutkuje koniecznością znalezienia pewnego mechanizmu pozwalającego na łatwiejsze zarządzaniem dużymi projektami. Najczęsciej spotykanym jest wzorzec MVC, który pozwala nam podzielić cały program na trzy odseparowane części. W przypadku aplikacji desktopowej Microsoft udostępnia nam ewolucję tego wzorca w postaci MVVM.',
'wpf_mvvm_introduction',
'2016-04-12T00:00:00');


INSERT INTO ArticleCategory (ArticleId, CategoryId) VALUES (
5,
1
);

INSERT INTO ArticleCategory (ArticleId, CategoryId) VALUES (
5,
4
);

INSERT INTO ArticleCategory (ArticleId, CategoryId) VALUES (
5,
5
);