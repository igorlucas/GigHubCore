using GigHubCore.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GigHubCore.Pages.Gigs
{
    public class InputModelGig
    {
        public int Id { get; set; }

        public Gig Gig { get; set; }

        public bool IsAttending { get; set; }

        public bool IsFollowing { get; set; }

        [Required(ErrorMessage = "O campo Local é obrigatório"), Display(Name = "Local")]
        [StringLength(225)]
        public string Venue { get; set; }

        [Required(ErrorMessage = "O campo Data é obrigatório"), Display(Name = "Data")]
        [DataType(DataType.Date), DateValidation(ErrorMessage = "O campo Data esta invalido")]
        public string Date { get; set; }

        [Required(ErrorMessage = "O campo Horário é obrigatório"), Display(Name = "Horário")]
        [DataType(DataType.Time), TimeValidation]
        public string Time { get; set; }

        [Required(ErrorMessage = "O campo Gênero é obrigatório"), Display(Name = "Gênero")]
        public int Genre { get; set; }

        public IList<Genre> Genres { get; set; }

        public DateTime DateTime
        {
            get { return DateTime.Parse(string.Format("{0} {1}", Date, Time)); }
        }

        public InputModelGig()
        {
            Genres = new List<Genre>();
        }
    }
}
