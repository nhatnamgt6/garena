using BLL.Interface;
using Common.DTO;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class RegisterService : RegisterInterface
    {
        private readonly GarenaContext _context;

        public RegisterService(GarenaContext context)
        {
            _context = context;
        }

        public async Task<ResponseDTO> Login(LoginDTO login)
        {
            var user = new User

            {
               
                UserName = login.UserName,
                Password = login.Password,
            };


            // Add user to the context
            _context.Set<User>().Add(user);
            await _context.SaveChangesAsync();

            return new ResponseDTO("User login successfully.", 200, true);
        }

       
        
        public async Task<ResponseDTO> Register(RegisterDTO register)
        {
            if (string.IsNullOrWhiteSpace(register.UserName))
            {
                return new ResponseDTO("Username cannot be blank.", 400, false);
            }

            if (string.IsNullOrWhiteSpace(register.Password))
            {
                return new ResponseDTO("Password cannot be blank.", 400, false);
            }

            if (register.Password != register.ConfirmPassword)
            {
                return new ResponseDTO("Password and Confirm Password do not match.", 400, false);
            }

            var user = new User

            {                
                UserName = register.UserName,
                Password = register.Password,
                ConfirmPassword = register.ConfirmPassword,
            };


            // Add user to the context
            _context.Set<User>().Add(user);
            await _context.SaveChangesAsync();

            return new ResponseDTO("User registered successfully.", 200, true);
        }


    }
}
