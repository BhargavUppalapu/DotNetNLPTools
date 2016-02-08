<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:tet="http://www.pdflib.com/XML/TET3/TET-3.0"
    xmlns:doc="http://www.capitaliq.com/Research/IntelligentTaggign/Document"
    exclude-result-prefixes="doc"
>
  <xsl:output method="xml" indent="yes" />
  <xsl:strip-space elements="*" />

  <xsl:template match="/">
    <xsl:apply-templates select="tet:TET/tet:Document" />
  </xsl:template>

  <xsl:template match="tet:Document">
    <xsl:element name="doc:Document">
      <xsl:apply-templates select="tet:DocInfo" />
      <xsl:apply-templates select="tet:Pages/tet:Page" />
    </xsl:element>
  </xsl:template>

  <xsl:template match="tet:DocInfo">
    <xsl:if test="tet:Author">
      <xsl:attribute name="Author">
        <xsl:value-of select="tet:Author" />
      </xsl:attribute>
    </xsl:if>

    <xsl:if test="tet:CreationDate">
      <xsl:attribute name="CreationDate">
        <xsl:value-of select="tet:CreationDate" />
      </xsl:attribute>
    </xsl:if>

    <xsl:if test="tet:Creator">
      <xsl:attribute name="Creator">
        <xsl:value-of select="tet:Creator" />
      </xsl:attribute>
    </xsl:if>

    <xsl:if test="tet:Subject">
      <xsl:attribute name="Subject">
        <xsl:value-of select="tet:Subject" />
      </xsl:attribute>
    </xsl:if>

    <xsl:if test="tet:Title">
      <xsl:attribute name="Title">
        <xsl:value-of select="tet:Title" />
      </xsl:attribute>
    </xsl:if>

  </xsl:template>
  
  <xsl:template match="tet:Page">
    <xsl:element name="doc:Page" >
      <xsl:apply-templates select="tet:Content/node()" />
    </xsl:element>
  </xsl:template>

  <xsl:template match="tet:Table">
    <xsl:element name="doc:Table" >
      <xsl:apply-templates select="tet:Row" />
    </xsl:element>
  </xsl:template>

  <xsl:template match="tet:Row">
    <xsl:element name="doc:Row" >
      <xsl:apply-templates select="tet:Cell" />
    </xsl:element>
  </xsl:template>

  <xsl:template match="tet:Cell">
    <xsl:element name="doc:Cell" >
      <xsl:if test="@colSpan">
        <xsl:attribute name="ColSpan">
          <xsl:value-of select="@colSpan" />
        </xsl:attribute>
      </xsl:if>

      <!--process all child nodes-->
      <xsl:apply-templates select="node()" />
    </xsl:element>
  </xsl:template>

  <xsl:template match="tet:Para">
    <xsl:element name="doc:Paragraph">
      <xsl:element name="doc:Text">
        <!--concatenate all child text nodes -->
        <xsl:apply-templates select="node()"/>
      </xsl:element>
    </xsl:element>
  </xsl:template>

  <xsl:template match="tet:Line">
    <xsl:apply-templates select="tet:Text"/>
  </xsl:template>
  
  <xsl:template match="tet:Word">
    <xsl:apply-templates select="tet:Text"/>
  </xsl:template>

  <xsl:template match="tet:Text">
    <xsl:value-of select="concat(normalize-space(.), ' ')" />
  </xsl:template>

  <xsl:template match="tet:Zone">
    <xsl:message terminate="yes">Element Zone is not supported yet. Use granularity=page or modify xslt</xsl:message>
  </xsl:template>

  <xsl:template match="tet:Glyph">
    <xsl:message terminate="yes">Element Glyph is not supported yet. Use granularity=page or modify xslt</xsl:message>
  </xsl:template>
</xsl:stylesheet>
