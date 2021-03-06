﻿using FormsAdminGP.Domain;
using FormsAdminGP.Services.DTO;
using FormsAdminGP.Services.Interfaces;
using FormsAdminGP.Services.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FormsAdminGP.RestfulAPI.Controllers
{
    [Produces("application/json")]
    [Route("landing/api-inforequest")]
    [AllowAnonymous]
    public class InfoRequestController : Controller
    {
        private readonly IInfoRequestService _infoRequestService;
        public InfoRequestController(IInfoRequestService infoRequestService)
        {
            _infoRequestService = infoRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = await _infoRequestService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("getby")]
        public async Task<IActionResult> GetByAsync(BaseRequest request)
        {
            var list = await _infoRequestService.GetByAsync(request);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var item = await _infoRequestService.GetByIdAsync(id);
            return Ok(item);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Post([FromBody]InfoRequestDto infoRequestDto)
        {
            if (ModelState.IsValid)
            {
                var item = await _infoRequestService.AddAsync(infoRequestDto);
                return Ok(item);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var item = await _infoRequestService.DeleteAsync(id);
            return Ok(item);
        }

        [HttpGet("exportToExcel")]
        public async Task<IActionResult> ExportToExcel(List<InfoRequestDto> listaInfo)
        {
            try
            {
                var excelStream = new MemoryStream();
                //var listaInfo = await _infoRequestService.GetByAsync(request);
                //distinct by
                await Task.Run(() => {

                });

                return null;

            }
            catch (Exception ex)
            {
                //-------------
                return NoContent();
            }



        }

        [HttpGet("download")]
        public async Task<IActionResult> CrearPagoXlsx(BaseRequest request)
        {
            try
            {
                var excelStream = new MemoryStream();
                var listaInfo = await _infoRequestService.GetByAsync(request);
                //distinct by
                var uniqueFormList = listaInfo.Select(x => new { x.FormHdId }).Distinct();

                FormatDataJson(ref listaInfo);

                using (var package = new ExcelPackage())
                {
                    var i = 0;
                    foreach (var item in uniqueFormList)
                    {
                        ++i;
                        var infoRequestList = listaInfo.Where(x => x.FormHdId == item.FormHdId);
                        var worksheet = package.Workbook.Worksheets.Add($"Form {i}");
                        var index = 0;

                        var infoRequest = infoRequestList.FirstOrDefault();
                        worksheet.Cells[$"A{index + 1}"].Value = "FECHA DE LA SOLICITUD";
                        worksheet.Cells[$"B{index + 1}"].Value = "LANDING PAGE";

                        worksheet.Cells[$"C{index + 1}"].Value = infoRequest.StatField?.Field1.Key?? string.Empty;
                        worksheet.Cells[$"D{index + 1}"].Value = infoRequest.StatField?.Field2.Key?? string.Empty;
                        worksheet.Cells[$"E{index + 1}"].Value = infoRequest.StatField?.Field3.Key?? string.Empty;
                        worksheet.Cells[$"F{index + 1}"].Value = infoRequest.StatField?.Field4.Key?? string.Empty;
                        worksheet.Cells[$"G{index + 1}"].Value = infoRequest.StatField?.Field5.Key?? string.Empty;
                        worksheet.Cells[$"H{index + 1}"].Value = infoRequest.StatField?.Field6.Key?? string.Empty;
                        worksheet.Cells[$"I{index + 1}"].Value = infoRequest.StatField?.Field7.Key?? string.Empty;
                        worksheet.Cells[$"J{index + 1}"].Value = infoRequest.StatField?.Field8.Key?? string.Empty;
                        worksheet.Cells[$"K{index + 1}"].Value = infoRequest.StatField?.Field9.Key?? string.Empty;
                        worksheet.Cells[$"L{index + 1}"].Value = infoRequest.StatField?.Field10.Key ?? string.Empty;
                        worksheet.Cells[$"M{index + 1}"].Value = infoRequest.StatField?.Field11.Key ?? string.Empty;
                        worksheet.Cells[$"N{index + 1}"].Value = infoRequest.StatField?.Field12.Key ?? string.Empty;
                        worksheet.Cells[$"O{index + 1}"].Value = infoRequest.StatField?.Field13.Key ?? string.Empty;
                        worksheet.Cells[$"P{index + 1}"].Value = infoRequest.StatField?.Field14.Key ?? string.Empty;
                        worksheet.Cells[$"Q{index + 1}"].Value = infoRequest.StatField?.Field15.Key ?? string.Empty;
                        worksheet.Cells[$"R{index + 1}"].Value = infoRequest.StatField?.Field16.Key ?? string.Empty;
                        worksheet.Cells[$"S{index + 1}"].Value = infoRequest.StatField?.Field17.Key ?? string.Empty;
                        worksheet.Cells[$"T{index + 1}"].Value = infoRequest.StatField?.Field18.Key ?? string.Empty;
                        worksheet.Cells[$"U{index + 1}"].Value = infoRequest.StatField?.Field19.Key ?? string.Empty;
                        worksheet.Cells[$"V{index + 1}"].Value = infoRequest.StatField?.Field20.Key ?? string.Empty;


                        foreach (var info in infoRequestList)
                        {
                            worksheet.Cells[$"A{index + 2}"].Value = info.RequestDateStr;
                            worksheet.Cells[$"B{index + 2}"].Value = info.LandingPageName;                            

                            if (!string.IsNullOrEmpty(info.StatField.Field1.Value))
                            {
                                worksheet.Cells[$"C{index + 2}"].Value = info?.StatField?.Field1.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field2.Value))
                            {
                                worksheet.Cells[$"D{index + 2}"].Value = info?.StatField?.Field2.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field3.Value))
                            {
                                worksheet.Cells[$"E{index + 2}"].Value = info?.StatField?.Field3.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field4.Value))
                            {
                                worksheet.Cells[$"F{index + 2}"].Value = info?.StatField?.Field4.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field5.Value))
                            {
                                worksheet.Cells[$"G{index + 2}"].Value = info?.StatField?.Field5.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field6.Value))
                            {
                                worksheet.Cells[$"H{index + 2}"].Value = info?.StatField?.Field6.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field7.Value))
                            {
                                worksheet.Cells[$"I{index + 2}"].Value = info?.StatField?.Field7.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field8.Value))
                            {
                                worksheet.Cells[$"J{index + 2}"].Value = info?.StatField?.Field8.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field9.Value))
                            {
                                worksheet.Cells[$"K{index + 2}"].Value = info?.StatField?.Field9.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field10.Value))
                            {
                                worksheet.Cells[$"L{index + 2}"].Value = info?.StatField?.Field10.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field11.Value))
                            {
                                worksheet.Cells[$"M{index + 2}"].Value = info?.StatField?.Field11.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field12.Value))
                            {
                                worksheet.Cells[$"N{index + 2}"].Value = info?.StatField?.Field12.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field13.Value))
                            {
                                worksheet.Cells[$"O{index + 2}"].Value = info?.StatField?.Field13.Value ?? string.Empty;
                             }

                            if (!string.IsNullOrEmpty(info.StatField.Field14.Value))
                            {
                                worksheet.Cells[$"P{index + 2}"].Value = info?.StatField?.Field14.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field15.Value))
                            {
                                worksheet.Cells[$"Q{index + 2}"].Value = info?.StatField?.Field15.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field16.Value))
                            {
                                worksheet.Cells[$"R{index + 2}"].Value = info?.StatField?.Field16.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field17.Value))
                            {
                                worksheet.Cells[$"S{index + 2}"].Value = info?.StatField?.Field17.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field18.Value))
                            {
                                worksheet.Cells[$"T{index + 2}"].Value = info?.StatField?.Field18.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field19.Value))
                            {
                                worksheet.Cells[$"U{index + 2}"].Value = info?.StatField?.Field19.Value ?? string.Empty;
                            }

                            if (!string.IsNullOrEmpty(info.StatField.Field20.Value))
                            {
                                worksheet.Cells[$"V{index + 2}"].Value = info?.StatField?.Field20.Value ?? string.Empty;
                            }
                            index++;
                        }
                        
                    }

                    // Send the file stream
                    package.SaveAs(excelStream);
                    excelStream.Position = 0;
                    // descargar spreadsheet
                    return File(excelStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "solicitudes.xlsx");
                }



            }
            catch (Exception ex)
            {
                //-------------
                return NoContent();
            }



        }

        private void FormatDataJson(ref IEnumerable<InfoRequestDto> listaInfo)
        {
            
            foreach (var item in listaInfo)
            {
                var objList = JsonConvert.DeserializeObject<List<FormDetailDto>>(item.InfoRequestData);
                int index = 0;
                var statField = new StatField();
                foreach (var _object in objList)
                {
                    index++;
                    var data = new KeyValuePair<string, string>(_object.FieldLabel, _object.Data);
                    switch (index)
                    {
                        case 1:
                            statField.Field1 = data;
                            break;
                        case 2:
                            statField.Field2 = data;
                            break;
                        case 3:
                            statField.Field3 = data;
                            break;
                        case 4:
                            statField.Field4 = data;
                            break;
                        case 5:
                            statField.Field5 = data;
                            break;
                        case 6:
                            statField.Field6 = data;
                            break;
                        case 7:
                            statField.Field7 = data;
                            break;
                        case 8:
                            statField.Field8 = data;
                            break;
                        case 9:
                            statField.Field9 = data;
                            break;
                        case 10:
                            statField.Field10 = data;
                            break;
                        case 11:
                            statField.Field11 = data;
                            break;
                        case 12:
                            statField.Field12 = data;
                            break;
                        case 13:
                            statField.Field13 = data;
                            break;
                        case 14:
                            statField.Field14 = data;
                            break;
                        case 15:
                            statField.Field15 = data;
                            break;
                        case 16:
                            statField.Field16 = data;
                            break;
                        case 17:
                            statField.Field17 = data;
                            break;
                        case 18:
                            statField.Field18 = data;
                            break;
                        case 19:
                            statField.Field19 = data;
                            break;
                        case 20:
                            statField.Field20 = data;
                            break;
                    }
                }

                item.StatField = statField;
            }
        }
    }






}
