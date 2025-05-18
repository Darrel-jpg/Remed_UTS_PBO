public interface IBuku
{
    void ShowInfo();
}

abstract class Buku : IBuku
{
    public string Judul { get; set; }
    public string Penulis { get; set; }
    public string TahunTerbit { get; set; }
    public bool Dipinjam { get; set; }

    public Buku(string judul, string penulis, string tahunTerbit)
    {
        Judul = judul;
        Penulis = penulis;
        TahunTerbit = tahunTerbit;
        Dipinjam = false;
    }

    public abstract void ShowInfo();

    public void Pinjam()
    {
        Dipinjam = true;
    }
    public void Kembalikan()
    {
        Dipinjam = false;
    }
    public void EditData(string judul, string penulis, string tahunTerbit)
    {
        Judul = judul;
        Penulis = penulis;
        TahunTerbit = tahunTerbit;
    }
}

class BukuFiksi : Buku
{
    public BukuFiksi(string judul, string penulis, string tahunTerbit) : base(judul, penulis, tahunTerbit)
    {
    }
    public override void ShowInfo()
    {
        Console.WriteLine($"[Fiksi] Judul: {Judul}, Penulis: {Penulis}, Tahun Terbit: {TahunTerbit}");
    }
}

class BukuNonFiksi : Buku
{
    public BukuNonFiksi(string judul, string penulis, string tahunTerbit) : base(judul, penulis, tahunTerbit)
    {
    }
    public override void ShowInfo()
    {
        Console.WriteLine($"[Non-Fiksi] Judul: {Judul}, Penulis: {Penulis}, Tahun Terbit: {TahunTerbit}");
    }
}

class Majalah : Buku
{
    public Majalah(string judul, string penulis, string tahunTerbit) : base(judul, penulis, tahunTerbit)
    {
    }
    public override void ShowInfo()
    {
        Console.WriteLine($"[Majalah] Judul: {Judul}, Penulis: {Penulis}, Tahun Terbit: {TahunTerbit}");
    }
}

class Petugas
{
    private List<Buku> daftarBuku = new List<Buku>();
    private List<Buku> bukuDipinjam = new List<Buku>();

    public void TambahBuku(Buku buku)
    {
        daftarBuku.Add(buku);
        Console.WriteLine($"Buku {buku.Judul} berhasil ditambahkan");
    }
    public void UbahBuku(int index, string judul, string penulis, string tahunTerbit)
    {
        if (index >= 0 && index < daftarBuku.Count)
        {
            daftarBuku[index].EditData(judul, penulis, tahunTerbit);
            Console.WriteLine($"Buku {daftarBuku[index].Judul} berhasil diubah");
        }
        else
        {
            Console.WriteLine("Index tidak valid");
        }
    }
    public void LihatDaftarBuku()
    {
        if (daftarBuku.Count == 0)
        {
            Console.WriteLine("Belum ada buku yang ditambahkan");
            return;
        }
        Console.WriteLine();
        for (int i = 0; i < daftarBuku.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            daftarBuku[i].ShowInfo();
        }
    }
    public void PinjamBuku(int index)
    {
        if (bukuDipinjam.Count >= 3)
        {
            Console.WriteLine("Batas peminjaman tercapai (maks 3 buku)");
            return;
        }
        var buku = daftarBuku[index];
        if (buku.Dipinjam)
        {
            Console.WriteLine("Buku sedang dipinjam");
            return;
        }
        else
        {
            buku.Pinjam();
            bukuDipinjam.Add(buku);
            Console.WriteLine($"Buku {buku.Judul} berhasil dipinjam");
        }
    }
    public void KembalikanBuku(int index)
    {
        var buku = bukuDipinjam[index];
        buku.Kembalikan();
        bukuDipinjam.RemoveAt(index);
        Console.WriteLine($"Buku {buku.Judul} berhasil dikembalikan");
    }
    public void LihatDaftarBukuDipinjam()
    {
        if (bukuDipinjam.Count == 0)
        {
            Console.WriteLine("Tidak ada buku yang dipinjam");
            return;
        }
        Console.WriteLine();
        for (int i = 0; i < bukuDipinjam.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            bukuDipinjam[i].ShowInfo();
        }
    }
}

class Program
{
    static void Main()
    {
        Petugas petugas = new Petugas();

        while (true)
        {
            Console.WriteLine("\n--- Sistem Manajemen Perpustakaan Mini---");
            Console.WriteLine("1. Tambah Buku");
            Console.WriteLine("2. Ubah Data Buku");
            Console.WriteLine("3. Lihat Buku");
            Console.WriteLine("4. Pinjam Buku");
            Console.WriteLine("5. Kembalikan Buku");
            Console.WriteLine("6. Lihat Daftar Buku Dipinjam");
            Console.WriteLine("7. Keluar");
            Console.Write("Pilih: ");
            int pilihan = Convert.ToInt32(Console.ReadLine());

            switch (pilihan)
            {
                case 1:
                    Console.Write("Jenis (1. Fiksi, 2. Non-Fiksi, 3. Majalah): ");
                    int jenisBuku = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Judul: ");
                    string judul = Console.ReadLine();
                    Console.Write("Penulis: ");
                    string penulis = Console.ReadLine();
                    Console.Write("Tahun Terbit: ");
                    string tahunTerbit = Console.ReadLine();

                    if (jenisBuku == 1)
                    {
                        petugas.TambahBuku(new BukuFiksi(judul, penulis, tahunTerbit));
                    }
                    else if (jenisBuku == 2)
                    {
                        petugas.TambahBuku(new BukuNonFiksi(judul, penulis, tahunTerbit));
                    }
                    else if (jenisBuku == 3)
                    {
                        petugas.TambahBuku(new Majalah(judul, penulis, tahunTerbit));
                    }
                    else
                    {
                        Console.WriteLine("Pilihan jenis buku tidak valid");
                    }
                    break;
                case 2:
                    petugas.LihatDaftarBuku();
                    Console.Write("Pilih nomor buku yang ingin diubah: ");
                    int indexUbah = Convert.ToInt32(Console.ReadLine()) - 1;
                    Console.Write("Judul baru: ");
                    string judulBaru = Console.ReadLine();
                    Console.Write("Penulis baru: ");
                    string penulisBaru = Console.ReadLine();
                    Console.Write("Tahun Terbit baru: ");
                    string tahunTerbitBaru = Console.ReadLine();
                    petugas.UbahBuku(indexUbah, judulBaru, penulisBaru, tahunTerbitBaru);
                    break;
                case 3:
                    Console.WriteLine("Daftar Buku:");
                    petugas.LihatDaftarBuku();
                    break;
                case 4:
                    petugas.LihatDaftarBuku();
                    Console.Write("Pilih nomor buku yang ingin dipinjam: ");
                    int indexPinjam = Convert.ToInt32(Console.ReadLine()) - 1;
                    petugas.PinjamBuku(indexPinjam);
                    break;
                case 5:
                    petugas.LihatDaftarBukuDipinjam();
                    Console.Write("Pilih nomor buku yang ingin dikembalikan: ");
                    int indexKembali = Convert.ToInt32(Console.ReadLine()) - 1;
                    petugas.KembalikanBuku(indexKembali);
                    break;
                case 6:
                    Console.WriteLine("Daftar Buku Dipinjam:");
                    petugas.LihatDaftarBukuDipinjam();
                    break;
                case 7:
                    Console.WriteLine("Keluar dari program");
                    return;
                default:
                    Console.WriteLine("Pilihan tidak valid");
                    break;
            }
        }
    }
}