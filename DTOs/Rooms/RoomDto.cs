using API.DTOs.Roles;
using BookingManagementApp.Models;

namespace API.DTOs.Rooms
{
    public class RoomDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int Floor {  get; set; }
        public int Capacity { get; set; }


        //membuat implicit operator untuk update
        public static implicit operator Room(RoomDto roomDto)
        {
            return new Room
            {
                Guid = roomDto.Guid,
                Name = roomDto.Name,
                Floor = roomDto.Floor,
                Capacity = roomDto.Capacity,
                ModifiedDate = DateTime.Now

            };
        }
        //membuat explicit operator untuk response get, create , getbyid
        public static explicit operator RoomDto(Room room)
        {
            return new RoomDto
            {
                Guid = room.Guid,
                Name = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity,

            };
        }
    }
}
