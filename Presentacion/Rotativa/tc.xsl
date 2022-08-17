<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="2.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:outline="http://wkhtmltopdf.org/outline" xmlns="http://www.w3.org/1999/xhtml">
    <xsl:output doctype-public="-//W3C//DTD XHTML 1.0 Strict//EN"
        doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" indent="yes" />
    <xsl:template match="outline:outline">
        <html>

        <head>
            <title>Table of Contents</title>
            <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
            <style>
              *{
              font-family: arial;
              }
              .ttoc {
              text-align: center;
              font-size: 16pt;
              font-family: arial;
              color: rgb(46,116,181);
              }

              body {
              font-size: 12pt;
              }

              li {
              list-style: none;
              margin-top: 12pt;
              margin-bottom: 12pt;
              }
              span {
              float: right;
              }
              li div {
                border-bottom: dotted 2px #000;
              }
              .toc > li{
                  padding:0;
              }
              .toc > li > ul > li > ul > li > ul > li {
                  display: none;
              }
            </style>
        </head>

        <body>
            <div class="ttoc">Tabla de Contenido</div>
            <ul class="toc">
                <xsl:apply-templates select="outline:item/outline:item" />
            </ul>
        </body>

        </html>
    </xsl:template>
    <xsl:template match="outline:item">
        <li>
            <xsl:if test="@title!=''">
                <div>
                    <a>
                        <xsl:if test="@link">
                            <xsl:attribute name="href">
                                <xsl:value-of select="@link" />
                            </xsl:attribute>
                        </xsl:if>
                        <xsl:if test="@backLink">
                            <xsl:attribute name="name">
                                <xsl:value-of select="@backLink" />
                            </xsl:attribute>
                        </xsl:if>
                        <xsl:value-of select="@title" />
                    </a>
                    <span>
                        <xsl:value-of select="@page" />
                    </span>
                </div>
            </xsl:if>
            <ul>
                <xsl:comment>added to prevent self-closing tags in QtXmlPatterns</xsl:comment>
                <xsl:apply-templates select="outline:item" />
            </ul>
        </li>
    </xsl:template>
</xsl:stylesheet>