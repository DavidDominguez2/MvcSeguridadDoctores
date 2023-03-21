using Microsoft.EntityFrameworkCore;
using MvcSeguridadDoctores.Data;
using MvcSeguridadDoctores.Models;

namespace MvcSeguridadDoctores.Repositories {
    public class RepositoryHospital {

        private HospitalContext context;

        public RepositoryHospital(HospitalContext context) {
            this.context = context;
        }

        ///PERFIL DOCTOR, GETENFERMOS, DELETEENFERMO(ID), FINDENFERMO
        public async Task<Doctor> PerfilDoctorAsync(string apellido) {
            return await this.context.Doctores.FirstOrDefaultAsync(x => x.Apellido == apellido);
        }

        public async Task<Doctor> PerfilDoctorAsync(int iddoctor) {
            return await this.context.Doctores.FirstOrDefaultAsync(x => x.DoctorNo == iddoctor);
        }

        public async Task<List<Enfermo>> GetEnfermosAsync() {
            return await this.context.Enfermos.ToListAsync();
        }

        public async Task DeleteEnfermoAsync(int idenfermo) {
            Enfermo? enfermo = this.context.Enfermos.FirstOrDefault(x => x.Inscripcion == idenfermo);
            if (enfermo != null) {
                this.context.Enfermos.Remove(enfermo);
                await this.context.SaveChangesAsync();
            }
        }

        public async Task<Doctor> ExisteDoctorAsync(string username, int password) {
            var consulta = this.context.Doctores.Where(x => x.Apellido == username && x.DoctorNo == password);
            return await consulta.FirstOrDefaultAsync();
        }

    }
}
