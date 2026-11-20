-- Datos de prueba para Lab13DB (ejecutar después de crear Grades y Courses)
USE Lab13DB;
GO
INSERT INTO Students (FirstName, LastName, Phone, Email, GradeId) VALUES
('Ana',    'Torres',   '987654321', 'ana.torres@tecsup.edu.pe',    1),
('Luis',   'Quispe',   '987111222', 'luis.quispe@tecsup.edu.pe',   1),
('Maria',  'Huaman',   '986333444', 'maria.huaman@tecsup.edu.pe',  2),
('Carlos', 'Mendoza',  '985555666', 'carlos.mendoza@tecsup.edu.pe',2),
('Andrea', 'Rojas',    '984777888', 'andrea.rojas@tecsup.edu.pe',  3),
('Jorge',  'Vargas',   '983999000', 'jorge.vargas@tecsup.edu.pe',  3);
GO
INSERT INTO Enrollments (StudentId, CourseId, [Date]) VALUES
(1, 1, '2026-03-02'), (1, 2, '2026-03-02'),
(2, 1, '2026-03-03'), (2, 3, '2026-03-03'),
(3, 2, '2026-03-04'), (3, 3, '2026-03-04'),
(4, 1, '2026-03-05'), (5, 2, '2026-03-05'),
(5, 3, '2026-03-06'), (6, 1, '2026-03-06');
GO
