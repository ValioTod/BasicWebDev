﻿using BasicWebServer.Server.HTTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.Responses
{
    public class TextFileResponse : Response
    {
        public string FileName { get; init; }

        public TextFileResponse(string fileName)
            : base(StatusCode.OK)
        {
            this.FileName = fileName;

            this.Headers.Add(Header.ContentType, ContentType.PlainText);
        }
        public override string ToString()
        {
            if (File.Exists(this.FileName))
            {
                this.Body = File.ReadAllTextAsync(this.FileName).Result;

                var fileByteCount = new FileInfo(this.FileName).Length;
                this.Headers.Add(Header.ContentLength, fileByteCount.ToString());

                this.Headers.Add(Header.ContentDisposition,
                    $"attachment; filename=\"{this.FileName}\"");
            }
            return base.ToString();
        }

    }
}
