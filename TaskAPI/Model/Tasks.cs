namespace TaskAPI.Model
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Tarefa { get; set; }
        public string Status { get; set; }  
        public DateTime DataTarefa { get; set; }
    }
}
