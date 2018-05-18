﻿using AbstractSushiBarModel;
using AbstractSushiBarService.BindingModels;
using AbstractSushiBarService.Interfaces;
using AbstractSushiBarService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractSushiBarService.ImplementationsBD
{
    public class VisitorServiceBD : IVisitorService
    {
        private AbstractDbContext context;

        public VisitorServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<VisitorViewModel> GetList()
        {
            List<VisitorViewModel> result = context.Visitors
                .Select(rec => new VisitorViewModel
                {
                    Id = rec.Id,
                    Mail = rec.Mail,
                    VisitorFIO = rec.VisitorFIO
                })
                .ToList();
            return result;
        }

        public VisitorViewModel GetElement(int id)
        {
            Visitor element = context.Visitors.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new VisitorViewModel
                {
                    Id = element.Id,
                    VisitorFIO = element.VisitorFIO,
                    Mail = element.Mail,
                    Messages = context.MessageInfos
                            .Where(recM => recM.VisitorId == element.Id)
                            .Select(recM => new MessageInfoViewModel
                            {
                                MessageId = recM.MessageId,
                                DateDelivery = recM.DateDelivery,
                                Subject = recM.Subject,
                                Body = recM.Body
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(VisitorBindingModel model)
        {
            Visitor element = context.Visitors.FirstOrDefault(rec => rec.VisitorFIO == model.VisitorFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Visitors.Add(new Visitor
            {
                VisitorFIO = model.VisitorFIO,
                Mail = model.Mail
            });
            context.SaveChanges();
        }

        public void UpdElement(VisitorBindingModel model)
        {
            Visitor element = context.Visitors.FirstOrDefault(rec =>
                                    rec.VisitorFIO == model.VisitorFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Visitors.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.VisitorFIO = model.VisitorFIO;
            element.Mail = model.Mail;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Visitor element = context.Visitors.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Visitors.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
