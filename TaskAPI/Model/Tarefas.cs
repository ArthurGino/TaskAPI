namespace TaskAPI.Model
{
    public class Tarefas
    {
        public int Id { get; set; }
        public string Tarefa { get; set; }
        public bool Status { get; set; }
        public DateTime DataTarefa { get; set; }
    }
}
