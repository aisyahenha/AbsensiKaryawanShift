# Absensi Karyawan Shift
Microservice Absensi Karyawan untuk Test Assesment Dikshatek-Mandiri

## Instalasi
1. Clone repository ini dan pastikan sudah terinstall **.NET 8**.
2. Setelah di-clone, jangan lupa untuk merestore NuGet packagenya.
3. Pastikan PostgreSQL dan SQL Server sudah terpasang dan running.
4. sesuaikan connection string pada appsettings.json. Absensi.Microservice menggunakan SQL Server dan User.Microservice PostgreSql.

## Arsitektur Microservices
Proyek ini diimplementasikan menggunakan arsitektur Microservices dengan dua API utama yang memiliki tanggung jawab terpisah. Berikut adalah rincian dari masing-masing API:

### API User Management
API ini bertanggung jawab untuk menangani autentikasi pengguna serta operasi CRUD untuk data pengguna. Berikut adalah endpoint-endpoint yang tersedia:

#### Endpoint Autentikasi
- **POST /login**: Endpoint untuk melakukan login.

#### Endpoint CRUD User
- **GET /user/get-all**: Mengambil semua data pengguna.
- **GET /user/detail/{id}**: Mengambil data pengguna berdasarkan ID.
- **POST /user/create**: Membuat pengguna baru.
- **PATCH /user/update**: Mengupdate data pengguna.
- **DELETE /user/delete/{id}**: Menghapus data pengguna berdasarkan ID.

### API Karyawan dan Absensi
API ini bertanggung jawab untuk menangani operasi CRUD untuk data karyawan serta operasi untuk data absensi karyawan. Berikut adalah endpoint-endpoint yang tersedia:

#### Endpoint CRUD Karyawan
- **GET /karyawan/get-all**: Mengambil semua data karyawan.
- **GET /karyawan/detail/{id}**: Mengambil data karyawan berdasarkan ID.
- **POST /karyawan/create**: Membuat karyawan baru.
- **PATCH /karyawan/update**: Mengupdate data karyawan.
- **DELETE /karyawan/delete/{id}**: Menghapus data karyawan berdasarkan ID.

#### Endpoint CRUD Absensi
- **GET /absensi/get-all**: Mengambil semua data absensi.
- **GET /absensi/detail/{id}**: Mengambil data absensi berdasarkan ID absensi.
- **GET /absensi/detail-karyawan/{id}**: Mengambil data absensi berdasarkan ID karyawan.
- **POST /absensi/absen-in/{idKaryawan}**: Melakukan insert data absen masuk karyawan.
- **POST /absensi/absen-out/{idKaryawan}**: Melakukan absen pulang untuk karyawan.

**Catatan**:
- Endpoint **/karyawan** dan **/absensi** untuk create, update, dan delete tidak dapat diakses tanpa autentikasi (login).
- Endpoint **/api/absensi/absen-in** dan **/api/absensi/absen-out** dapat diakses oleh publik.
- Saat pertama kali menjalankan program, Anda dapat menggunakan akun default dengan username: "admin" dan password: "admin123". Seeder telah disediakan untuk membuat akun ini secara otomatis.

## RDBMS Design

Proyek ini menggunakan Relational Database Management System (RDBMS) untuk mengelola data. Berikut adalah deskripsi mengenai tabel-tabel yang ada dalam database ini:

### Tabel User
Tabel **User** digunakan untuk menyimpan informasi tentang pengguna sistem. Tabel ini tidak memiliki relasi dengan tabel lain. Berikut adalah struktur tabel **User**:
- **Id**: Primary key, tipe data integer, auto increment.
- **Username**: Tipe data string, digunakan untuk menyimpan nama pengguna.
- **Password**: Tipe data string, digunakan untuk menyimpan kata sandi pengguna.
- **Role**: Tipe data string, digunakan untuk menyimpan peran pengguna dalam sistem.

### Tabel Karyawan
Tabel **Karyawan** digunakan untuk menyimpan informasi tentang karyawan. Berikut adalah struktur tabel **Karyawan**:
- **Id**: Primary key, tipe data integer, auto increment.
- **NIK**: Tipe data string, digunakan untuk menyimpan Nomor Induk Karyawan.
- **Name**: Tipe data string, digunakan untuk menyimpan nama karyawan.
- **Address**: Tipe data string, digunakan untuk menyimpan alamat karyawan.
- **DateOfBirth**: Tipe data date, digunakan untuk menyimpan tanggal lahir karyawan.
- **Position**: Tipe data string, digunakan untuk menyimpan posisi atau jabatan karyawan.
- **Shift**: Tipe data bool, digunakan untuk menyimpan shift kerja karyawan.
- **ShiftDescription**: Tipe data string, digunakan untuk menyimpan deskripsi shift kerja karyawan.
- **CreatedAt**: Tipe data datetime, digunakan untuk menyimpan waktu pembuatan data karyawan.
- **UpdatedAt**: Tipe data datetime, digunakan untuk menyimpan waktu terakhir data karyawan diperbarui.

### Tabel Absensi
Tabel **Absensi** digunakan untuk menyimpan informasi tentang absensi karyawan. Tabel ini memiliki relasi banyak ke satu (n:1) dengan tabel **Karyawan**, yang berarti satu karyawan dapat memiliki banyak data absensi. Berikut adalah struktur tabel **Absensi**:
- **Id**: Primary key, tipe data integer, auto increment.
- **KaryawanId**: Foreign key, tipe data integer, mengacu pada kolom **Id** di tabel **Karyawan**.
- **DateIn**: Tipe data date, digunakan untuk menyimpan tanggal absensi masuk.
- **TimeIn**: Tipe data time, digunakan untuk menyimpan waktu absensi masuk.
- **StatusMasuk**: Tipe data string, digunakan untuk menyimpan status absensi masuk.
- **TimeOut**: Tipe data time, digunakan untuk menyimpan waktu absensi keluar.
- **StatusKeluar**: Tipe data string, digunakan untuk menyimpan status absensi keluar.
- **SelisihJamMasukKeluar**: Tipe data integer, digunakan untuk menyimpan selisih jam antara absensi masuk dan keluar.

### Relasi Antar Tabel
- **Karyawan (1) - (n) Absensi**: Satu karyawan dapat memiliki banyak data absensi, yang diidentifikasi oleh kolom **KaryawanId** di tabel **Absensi** yang mengacu pada kolom **Id** di tabel **Karyawan**.

