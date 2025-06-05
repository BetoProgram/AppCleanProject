--
-- Creación de la tabla de Roles
--
CREATE TABLE Roles (
    id SERIAL PRIMARY KEY,
    role_name VARCHAR(50) NOT NULL UNIQUE,
    description TEXT
);

--
-- Creación de la tabla de Usuarios
--
CREATE TABLE Users (
    id BIGSERIAL PRIMARY KEY,
    first_name VARCHAR(100) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL, -- Almacenar hashes de contraseñas, no texto plano
    phone_number VARCHAR(20),
    is_active BOOLEAN NOT NULL DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

--
-- Tabla de unión para Roles de Usuario (muchos a muchos)
--
CREATE TABLE UserRoles (
    user_id BIGINT NOT NULL,
    role_id INT NOT NULL,
    PRIMARY KEY (user_id, role_id),
    FOREIGN KEY (user_id) REFERENCES Users(id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (role_id) REFERENCES Roles(id) ON DELETE RESTRICT ON UPDATE CASCADE
);

--
-- Creación de la tabla de Especialidades Veterinarias
--
CREATE TABLE Specialties (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE,
    description TEXT
);

--
-- Creación de la tabla de Veterinarios (extiende la tabla Users)
-- Un veterinario es un usuario con el rol de veterinario.
--
CREATE TABLE Veterinarians (
    id BIGINT PRIMARY KEY, -- FK a Users(id)
    specialty_id INT,
    license_number VARCHAR(50) UNIQUE,
    bio TEXT,
    photo_url VARCHAR(255),
    FOREIGN KEY (id) REFERENCES Users(id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (specialty_id) REFERENCES Specialties(id) ON DELETE SET NULL ON UPDATE CASCADE
);

--
-- Creación de la tabla de Servicios
--
CREATE TABLE Services (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL UNIQUE,
    description TEXT,
    duration_minutes INTEGER NOT NULL CHECK (duration_minutes > 0),
    price NUMERIC(10,2) NOT NULL CHECK (price >= 0),
    is_active BOOLEAN NOT NULL DEFAULT TRUE
);

--
-- Creación de la tabla de Mascotas
--
CREATE TABLE Pets (
    id BIGSERIAL PRIMARY KEY,
    owner_id BIGINT NOT NULL,
    name VARCHAR(100) NOT NULL,
    species VARCHAR(100) NOT NULL,
    breed VARCHAR(100),
    date_of_birth DATE,
    gender VARCHAR(20), -- e.g., 'Male', 'Female', 'Unknown'
    characteristics TEXT,
    photo_url VARCHAR(255),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (owner_id) REFERENCES Users(id) ON DELETE CASCADE ON UPDATE CASCADE
);

--
-- Creación de la tabla de Horarios de Veterinarios
-- Aquí se define la disponibilidad general de cada veterinario (e.g., Lunes 9-5)
--
CREATE TABLE VeterinarySchedules (
    id BIGSERIAL PRIMARY KEY,
    veterinarian_id BIGINT NOT NULL,
    day_of_week VARCHAR(15) NOT NULL, -- e.g., 'Monday', 'Tuesday'
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,
    is_available BOOLEAN NOT NULL DEFAULT TRUE,
    notes TEXT,
    UNIQUE (veterinarian_id, day_of_week, start_time, end_time), -- Evitar horarios duplicados
    FOREIGN KEY (veterinarian_id) REFERENCES Veterinarians(id) ON DELETE CASCADE ON UPDATE CASCADE,
    CHECK (end_time > start_time)
);

--
-- Creación de la tabla de Estados de Citas
--
CREATE TABLE AppointmentStatuses (
    id SERIAL PRIMARY KEY,
    status_name VARCHAR(50) NOT NULL UNIQUE, -- e.g., 'Pending', 'Confirmed', 'Cancelled', 'Completed', 'Rejected'
    description TEXT
);

--
-- Creación de la tabla de Citas
--
CREATE TABLE Appointments (
    id BIGSERIAL PRIMARY KEY,
    client_id BIGINT NOT NULL,
    pet_id BIGINT NOT NULL,
    veterinarian_id BIGINT NOT NULL,
    service_id INT NOT NULL,
    status_id INT NOT NULL,
    appointment_datetime TIMESTAMP WITH TIME ZONE NOT NULL,
    reason_for_appointment TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (client_id) REFERENCES Users(id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (pet_id) REFERENCES Pets(id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (veterinarian_id) REFERENCES Veterinarians(id) ON DELETE RESTRICT ON UPDATE CASCADE, -- RESTRICT para no borrar veterinarios con citas
    FOREIGN KEY (service_id) REFERENCES Services(id) ON DELETE RESTRICT ON UPDATE CASCADE, -- RESTRICT para no borrar servicios con citas
    FOREIGN KEY (status_id) REFERENCES AppointmentStatuses(id) ON DELETE RESTRICT ON UPDATE CASCADE,
    UNIQUE (veterinarian_id, appointment_datetime) -- Un veterinario solo puede tener una cita a la vez
);

--
-- Índices para mejorar el rendimiento de las consultas
--
CREATE INDEX idx_users_email ON Users(email);
CREATE INDEX idx_veterinarians_license ON Veterinarians(license_number);
CREATE INDEX idx_pets_owner_id ON Pets(owner_id);
CREATE INDEX idx_appointments_client_id ON Appointments(client_id);
CREATE INDEX idx_appointments_veterinarian_id ON Appointments(veterinarian_id);
CREATE INDEX idx_appointments_datetime ON Appointments(appointment_datetime);
CREATE INDEX idx_veterinaryschedules_veterinarian_id ON VeterinarySchedules(veterinarian_id);

--
-- Datos iniciales (opcional, pero recomendado para Roles y Estados de Cita)
--
INSERT INTO Roles (role_name, description) VALUES
('Administrator', 'Full control over the system.'),
('Veterinarian', 'Manages appointments, patients, and schedules.'),
('Client', 'Schedules appointments for their pets.');

INSERT INTO AppointmentStatuses (status_name, description) VALUES
('Pending', 'Appointment requested, awaiting confirmation.'),
('Confirmed', 'Appointment has been confirmed by the clinic/veterinarian.'),
('Cancelled', 'Appointment was cancelled by client or clinic.'),
('Completed', 'Appointment has taken place.'),
('Rejected', 'Appointment request was denied by the clinic/veterinarian.');

-- Consulta General de Salud
INSERT INTO Services (name, description, duration_minutes, price, is_active) VALUES
('Consulta General', 'Revisión completa de la salud de la mascota, incluye examen físico y recomendaciones generales.', 30, 350.00, TRUE);

-- Vacunación (ejemplo específico para la vacuna séxtuple canina)
INSERT INTO Services (name, description, duration_minutes, price, is_active) VALUES
('Vacunación Séxtuple Canina', 'Aplicación de la vacuna séxtuple para perros (moquillo, parvovirus, hepatitis, adenovirus, parainfluenza, leptospirosis).', 15, 450.00, TRUE);

-- Desparasitación
INSERT INTO Services (name, description, duration_minutes, price, is_active) VALUES
('Desparasitación Interna/Externa', 'Administración de medicamento para el control de parásitos internos y/o externos.', 20, 280.00, TRUE);

-- Cirugía Menor (ej. sutura de heridas)
INSERT INTO Services (name, description, duration_minutes, price, is_active) VALUES
('Cirugía Menor (Sutura)', 'Procedimiento quirúrgico para suturar heridas leves o moderadas.', 60, 1200.00, TRUE);

-- Estética y Peluquería
INSERT INTO Services (name, description, duration_minutes, price, is_active) VALUES
('Estética y Peluquería Canina (Razas Pequeñas)', 'Servicio de baño, corte de pelo y limpieza general para perros de razas pequeñas.', 90, 500.00, TRUE);

-- Limpieza Dental
INSERT INTO Services (name, description, duration_minutes, price, is_active) VALUES
('Limpieza Dental (Profilaxis)', 'Remoción de sarro y pulido dental bajo sedación.', 90, 1800.00, TRUE);

-- Consulta de Especialidad (ej. Dermatología)
INSERT INTO Services (name, description, duration_minutes, price, is_active) VALUES
('Consulta Dermatológica', 'Evaluación y diagnóstico de problemas de piel y pelaje.', 45, 600.00, TRUE);

-- Prueba de Laboratorio (ej. Hemograma Completo)
INSERT INTO Services (name, description, duration_minutes, price, is_active) VALUES
('Análisis de Hemograma Completo', 'Extracción de muestra y análisis de sangre para hemograma completo.', 15, 300.00, TRUE);

-- Eutanasia (ejemplo de servicio sensible, pero necesario)
INSERT INTO Services (name, description, duration_minutes, price, is_active) VALUES
('Eutanasia Humanitaria', 'Procedimiento para finalizar la vida de una mascota de forma compasiva.', 30, 900.00, TRUE);

-- Control de Peso y Nutrición
INSERT INTO Services (name, description, duration_minutes, price, is_active) VALUES
('Asesoría Nutricional y Control de Peso', 'Evaluación de dieta y plan personalizado para el manejo del peso de la mascota.', 40, 400.00, TRUE);

-- Servicio Inactivo (ejemplo de servicio que no está disponible temporalmente)
INSERT INTO Services (name, description, duration_minutes, price, is_active) VALUES
('Rayos X (Digital)', 'Toma de radiografías digitales para diagnóstico.', 25, 750.00, FALSE);