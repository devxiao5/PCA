
<HTML><TITLE></TITLE>
<img src="/Graphics/PCALOGO.gif">
<SCRIPT LANGUAGE="Javascript">
function closew() {
  window.history.go(-1)
}
function esign() {
  window.open("http://p-ca.com/cgi-bin/db2www/sign_contract.txt/process?job=$(job)&group=$(group)&seq=$(seq)&ckey=$(ckey)&phase=$(phase)&type=$(type)","SignContract","TOP=0,LEFT=0,HEIGHT=400,WIDTH=450,alwaysRaised,toolbars")
}
function change1() {
      document.img1.src = "/GRAPHICS/signit.gif"
    }
function change11() {
      document.img1.src = "/GRAPHICS/report2.gif"
  }
</SCRIPT>
<body>
<cfquery name="get_po" dataSource="PCA_MAST">
  		select *
  		from POMASTER
        where POMASTER_COMPANY_NO = '#url.comid#' and POMASTER_WORKMASTER_ID = '#url.workid#' and POMASTER_GROUP_NO = '#url.groupno#' and POMASTER_SEQ_NO = '#url.seqno#' and POMASTER_CV_ID = '#url.cvid#'
 </cfquery>
<font color="black"><div align="center"><font size="2">
<b>PRIME CONTRACT BETWEEN</b><br>
<b>OWNER AND CONTRACTOR</b><br>
<b>(P.O # <cfoutput>#url.comid#-#url.workid#-#url.groupno#-#url.seqno#-#url.cvid#</cfoutput>)</b><br></div>

<p><B>OWNER AND CONTRACTOR enter into this Prime Contract as of  $(hold_contract_lit_month) $(hold_contract_day), $(hold_contract_year).<p>
<TABLE  width="153%" cellpadding="0" cellspacing="0"><TR>
   <TD><b>   OWNER:</b></TD>
   <TD><b>CONTRACTOR:</b></TD></TR>
</TABLE>
@readjob(job)
@getclient(hold_job_client)
@DTW_CONCAT(hold_client_city," ,",hold_client_city_comma)
@DTW_CONCAT(hold_client_city_comma,hold_client_state,hold_client_city_comma_state)
@DTW_CONCAT(hold_client_city_comma_state," ",hold_client_city_comma_state)
@DTW_CONCAT(hold_client_city_comma_state,hold_client_zip,hold_client_city_comma_state_zip)
@DTW_CONCAT(hold_contractor_city," ,",hold_contractor_city_comma)
@DTW_CONCAT(hold_contractor_city_comma,hold_contractor_state,hold_contractor_city_comma_state)
@DTW_CONCAT(hold_contractor_city_comma_state," ",hold_contractor_city_comma_state)
@DTW_CONCAT(hold_contractor_city_comma_state,hold_contractor_zip,hold_contractor_city_comma_state_zip)
@DTW_CONCAT("Phone: ",hold_contractor_phone,hold_c_phone)
@DTW_CONCAT(" Fax: ",hold_contractor_fax,hold_c_fax)
@DTW_CONCAT(hold_c_phone,hold_c_fax,hold_c_phone_fax)
<TABLE border="0" width="150%" cellpadding="0" cellspacing="0">
   <TR><TD align="left" width="18%">$(hold_job_client)</TD><TD width="7%"></TD><TD align="left" width="47%">$(hold_contractor_name)</TD></TR>
   <TR><TD align="left" width="15%">$(hold_client_address)</TD><TD width="10%"></TD><TD align="left" width="40%">$(hold_contractor_address)</TD></TR>
   <TR><TD align="left" width="15%">$(hold_client_city_comma_state_zip)</TD><TD align="left" width="10%"></TD><TD align="left" width="40%">$(hold_contractor_city_comma_state_zip)</TD></TR>
</TABLE>
<TABLE width="100%" cellpadding="0" cellspacing="0">
   <TR><TH width="56%"><TD align="left" width="7%">Contact:</TD><TD align="left" width="34%">$(hold_contractor_contact)</TD></TABLE>
<TABLE width="100%" cellpadding="0" cellspacing="0">
   <TR><TH width="56%"><TD align="left" width="41%">$(hold_c_phone_fax)</TD></TABLE><br>
<TABLE  width="85%" cellpadding="0" cellspacing="0">
   <TR><TD><b>   PROGRAM MANAGER:</b></TD><TD><b>ARCHITECT:</b></TD></TR></TABLE>
%IF ($(hold_job_architect) == "I")
   @getsec(hold_job_architect_inhouse)
   <TABLE border="0" width="135%" cellpadding="0" cellspacing="0">
     <TR><TD align="left" width="20%">PLANNING & CONSTRUCTION ASSOC., INC.</TD><td align="left" width="130%">PLANNING & CONSTRUCTION ASSOC., INC.</TD></TR>
     <TR><TD align="left" width="41%">1200 Delor Avenue</TD><TD align="left" width="109%">1200 Delor Avenue</TD></TR>
     <TR><TD align="left" width="41%">Louisville, KY 40217</TD><TD align="left" width="109%">Louisville, KY 40217</TD></TR></TABLE>
   <TABLE width="100%" cellpadding="0" cellspacing="0">
     <TR><TH width="56%"><TD align="left" width="7%">Contact:</TD><TD align="left" width="34%">$(architect_name)</TD></TABLE>
   <TABLE width="100%" cellpadding="0" cellspacing="0">
     <TR><TH width="56%"><TD align="left" width="41%">Phone:502/636-2334 Fax:502/636-3406</TD></TABLE><br>
%ELIF ($(hold_job_architect) == "C")
   @getarchitect(hold_job_architect_consulting)
   @DTW_CONCAT(hold_consulting_city," ,",hold_consulting_city_comma)
   @DTW_CONCAT(hold_consulting_city_comma,hold_consulting_state,hold_consulting_city_comma_state)
   @DTW_CONCAT(hold_consulting_city_comma_state," ",hold_consulting_city_comma_state)
   @DTW_CONCAT(hold_consulting_city_comma_state,hold_consulting_zip,hold_consulting_city_comma_state_zip)
   <TABLE border="0" width="135%" cellpadding="0" cellspacing="0">
     <TR><TD align="left" width="20%">PLANNING & CONSTRUCTION ASSOC., INC.</TD><TD align="left" width="130%">$(hold_job_architect_consulting)</TD></TR>
     <TR><TD align="left" width="41%">1200 Delor Avenue</TD><TD align="left" width="109%">$(hold_consulting_address)</TD></TR>
     <TR><TD align="left" width="41%">Louisville, KY 40217</TD><TD align="left" width="109%">$(hold_consulting_city_comma_state_zip)</TD></TR></TABLE>
   <TABLE width="100%" cellpadding="0" cellspacing="0">
     <TR><TH width="56%"><TD align="left" width="7%">Contact:</TD><TD align="left" width="34%">$(hold_consulting_contact)</TD></TABLE>
   @DTW_CONCAT("Phone: ",hold_consulting_phone,hold_c_phone)
   @DTW_CONCAT(" Fax: ",hold_consulting_fax,hold_c_fax)
   @DTW_CONCAT(hold_c_phone,hold_c_fax,hold_c_phone_fax)
   <TABLE width="100%" cellpadding="0" cellspacing="0">
     <TR><TH width="56%"><TD align="left" width="41%">$(hold_c_phone_fax)</TD></TABLE><br>
%ENDIF

</b>PROJECT : $(hold_job_client) / $(hold_job_location) - $(hold_job_description) / $(hold_job_class) / $(hold_job_type)<br>
SITE : $(hold_job_address), $(hold_job_location), $(hold_job_location_state)  $(hold_job_location_zip)
<br><br>
%IF ($(hold_add_scope) == "Y")
    <b>I. SCOPE OF THE WORK.</b> Contractor shall perform all Work described in the Contract Documents as an Independent Contractor and not as an
     employee of Owner or Program Manager.  Contractor agrees to take all necessary measures to obtain and maintain such Independent Contractor status
     at all times and prior to commencement of Work hereunder and to provide Owner and Program Manager documentary evidence of the same prior to the commencement
     of Work hereunder.
%ELSE
    <b>I. SCOPE OF THE WORK.</b> Contractor shall perform all Work described in the Contract Documents.
%ENDIF
The Work is generally described as follow:
@getpo(job,group,seq,ckey,phase)
<u>$(hold_detail_desc)</u><p>
<TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
@comma(hold_depre_site,hold_comma)
   <TR><TD align="left" width="25%"><B>COST ALLOCATION:</b></TD><TD width="25%"><b>SITE DEVELOPMENT</TD><TD align="right" width="20%"><b>$ $(hold_comma)</TD><TD width="35%"></TD></TR>
@comma(hold_depre_bldg,hold_comma)
   <TR><TD align="left" width="25%"></TD><TD width="25%"><b>BUILDING</TD><TD align="right" width="20%"><b>$ $(hold_comma)</TD><TD width="35%"></TD></TR>
@comma(hold_depre_equip,hold_comma)
   <TR><TD align="left" width="25%"></TD><TD width="25%"><b>EQUIPMENT</TD><TD align="right" width="20%"><b>$ $(hold_comma)</TD><TD width="35%"></TD></TR>
@comma(hold_depre_misc,hold_comma)
   <TR><TD align="left" width="25%"></TD><TD width="25%"><b>MISCELLANEOUS</TD><TD align="right" width="20%"><b>$ $(hold_comma)</TD><TD width="35%"></TD></TR></TABLE>
<br>
The general description of the Work in the foregoing sentence is merely for reference, and does not limit any of the Work set forth in the Contract Documents.
<br><br>
<b>II. DATE OF COMMENCEMENT AND SUBSTANTIAL COMPLETION.</b> The Work shall be commenced by Contractor on the date specified in the Notice to Proceed to be issued by Owner.
Contractor shall have <u>$(hold_days_literal) ($(hold_days))</u> working days after the commencement date to achieve Substantial Completion.
<br><br>
@comma(hold_amt,hold_comma)
<b>III. CONTRACT PRICE.</b> Owner shall pay Contractor for completion of the Work in accordance with the Contract Documents, the lump sum of <u>$(hold_amt_literal) ($ $(hold_comma)),</u> subject to additions and deductions
as provided in the Contract Documents.
<br><br>
%IF ($(hold_retainage) == "0")
   @DTW_ASSIGN(hold_retainage_literal,"Zero")
%ELIF ($(hold_retainage) == "1")
   @DTW_ASSIGN(hold_retainage_literal,"One")
%ELIF ($(hold_retainage) == "2")
   @DTW_ASSIGN(hold_retainage_literal,"Two")
%ELIF ($(hold_retainage) == "3")
   @DTW_ASSIGN(hold_retainage_literal,"Three")
%ELIF ($(hold_retainage) == "4")
   @DTW_ASSIGN(hold_retainage_literal,"Four")
%ELIF ($(hold_retainage) == "5")
   @DTW_ASSIGN(hold_retainage_literal,"Five")
%ELIF ($(hold_retainage) == "6")
   @DTW_ASSIGN(hold_retainage_literal,"Six")
%ELIF ($(hold_retainage) == "7")
   @DTW_ASSIGN(hold_retainage_literal,"Seven")
%ELIF ($(hold_retainage) == "8")
   @DTW_ASSIGN(hold_retainage_literal,"Eight")
%ELIF ($(hold_retainage) == "9")
   @DTW_ASSIGN(hold_retainage_literal,"Nine")
%ELIF ($(hold_retainage) == "10")
   @DTW_ASSIGN(hold_retainage_literal,"Ten")
%ELIF ($(hold_retainage) == "11")
   @DTW_ASSIGN(hold_retainage_literal,"Eleven")
%ELIF ($(hold_retainage) == "12")
   @DTW_ASSIGN(hold_retainage_literal,"Twelve")
%ELIF ($(hold_retainage) == "13")
   @DTW_ASSIGN(hold_retainage_literal,"Thirteen")
%ELIF ($(hold_retainage) == "14")
   @DTW_ASSIGN(hold_retainage_literal,"Fourteen")
%ELIF ($(hold_retainage) == "15")
   @DTW_ASSIGN(hold_retainage_literal,"Fifteen")
%ELIF ($(hold_retainage) == "16")
   @DTW_ASSIGN(hold_retainage_literal,"Sixteen")
%ELIF ($(hold_retainage) == "17")
   @DTW_ASSIGN(hold_retainage_literal,"Seventeen")
%ELIF ($(hold_retainage) == "18")
   @DTW_ASSIGN(hold_retainage_literal,"Eighteen")
%ELIF ($(hold_retainage) == "19")
   @DTW_ASSIGN(hold_retainage_literal,"Nineteen")
%ELIF ($(hold_retainage) == "20")
   @DTW_ASSIGN(hold_retainage_literal,"Twenty")
%ENDIF
<b>IV. PAYMENT PROCEDURES.<br>
<TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
   <TR><TD align="left" width="6%"><b>4.1</TD><TD width="99%"><b>Progress Payments.</b></TD></TR>
   <TR><TD align="left" width="6%"></TD><TD width="99%"><b>A.</b> Based upon properly submitted Applications and Certificate for Payment (AIA Document G702) submitted by the Contractor to Program Manager
   , Owner shall make progress payments at monthly intervals on account of the Contract price.</TD></TR>
   <TR><TD align="left" width="6%"></TD><TD width="99%"><b>B.</b> Unless otherwise specified in the Contract Documents, the period covered by each Application for payment shall be one calendar month ending on the last
   day of the month. Applications for Payment shall be due on or before the $(hold_bill_day)$(hold_bill_suffix) of the each month.</TD></TR>
   <TR><TD align="left" width="6%"></TD><TD width="99%"><b>C.</b> Owner will approve or disapprove a properly submitted Application for Payment within Fourteen (14) days after receipt. Owner will make payment on an approved
   Application for Payment not later than Twenty-one (21) days after such approval.</TD></TR>
   <TR><TD align="left" width="6%"></TD><TD width="99%"><b>D.</b> Owner shall retain $(hold_retainage_literal) percent ($(hold_retainage)%) of the aggregate amount due Contractor on approved Application and Certificate for Payment until Final Completion has
   been achieved, at which time no additional amounts shall be retained, except for amounts which Owner is entitled to retain under the Contract Documents.</TD></TR>
   <TR><TD align="left" width="6%" valign="top"><b>4.2</TD><TD width="99%"><b>Final Payment.</b> Final payment, constituting the entire unpaid balance of the Contract Price, shall be made by Owner to the Contractor when all obligations
   of the Contractor under the Contract Documents have been fully performed by the Contractor, including without limitation all items on the Punch List described in the General Conditions. Such Final Payment shall be made by Owner
   not more than Thirty (30) days after all of the foregoing conditions have been fully satisfied.</TD></TR></TABLE><br><br>
<b>V. MISCELLANEOUS PROVISIONS</b>
%IF (hold_po_type == "S")
   <TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
     <TR><TD align="left" width="6%" valign="top"><b>5.1</TD><TD width="99%"><b>Entire Agreement. </b> The Contract Documents form the entire and integrated agreement between the Owner and Contractor and supersede prior negotiations,
     representations or agreements, either written or oral. The Contract Documents may be amended or modified only by a Change Order, Modification of Contract, Construction Change Directive or minor change in Work(collectively, a
     "Modification") or, in the case of this Prime Contract, in a written agreement signed by Owner and Contractor. Any provisions in documents submitted by Contractor which purport to limit or excuse Contractor's obligation under the
     Contract Documents shall be of no force or effect unless Owner has accepted such limitation or excuse in writing.</TD></TR>
     <TR><TD align="left" width="6%" valign="top"><b>5.2</TD><TD width="99%"><b>Construction of Contract Documents. </b> If there is any inconsistency between the terms of this Prime Contract and the General Conditions, the terms of this
     Prime Contract shall govern. If there is any inconsistency between the terms of the General Conditions and any other of the Contract Documents, including the Specifications and Special Conditions, the General Conditions shall govern.</TD></TR>
     <TR><TD align="left" width="6%" valign="top"><b>5.3</TD><TD width="99%"><b>Notices. </b> Written notice shall be deemed to have been duly served when delivered in person, by telecopy, by express delivery or by registered or certified
     mail to the addresses shown above, or to such other address a party may hereafter designate by written notice.</TD></TR></TABLE>
%ELIF (hold_po_type == "G")
    <TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
      <TR><TD align="left" width="6%" valign="top"><b>5.1</TD><TD width="99%"><b>Entire Agreement. </b> The Contract Documents form the entire and integrated agreement between the Owner and Contractor and supersede prior negotiations,
      representations or agreements, either written or oral. The Contract Documents may be amended or modified only by a Change Order, Modification of Contract, Construction Change Directive or minor change in Work(collectively, a
      "Modification") or, in the case of this Prime Contract, in a written agreement signed by Owner and Contractor. Any provisions in documents submitted by Contractor which purport to limit or excuse Contractor's obligation under the
      Contract Documents shall be of no force or effect unless Owner has accepted such limitation or excuse in writing.</TD></TR>
      <TR><TD align="left" width="6%" valign="top"><b>5.2</TD><TD width="99%"><b>Construction of Contract Documents. </b> If there is any inconsistency between the terms of this Prime Contract and the General Conditions, the terms of this
      Prime Contract shall govern. If there is any inconsistency between the terms of the General Conditions and any other of the Contract Documents, including the Specifications and Special Conditions, the General Conditions shall govern.</TD></TR>
      <TR><TD align="left" width="6%" valign="top"><b>5.3</TD><TD width="99%"><b>Capitalized Terms. </b>All capitalized terms used in this Prime Contract and not defined herein shall have the meaning ascribed to them in the General Conditions
      or elsewhere in the Contract Documents.</TD></TR>
      <TR><TD align="left" width="6%" valign="top"><b>5.4</TD><TD width="99%"><b>Notices. </b> Written notice shall be deemed to have been duly served when delivered in person, by telecopy, by express delivery or by registered or certified
      mail to the addresses shown above, or to such other address a party may hereafter designate by written notice.</TD></TR></TABLE>
%ENDIF
<br>
<b>VI. INSURANCE AND BONDS</b>
<TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
  <TR><TD align="left" width="6%" valign="top"><b>6.1</TD><TD width="99%"><b>Named Insureds. </b> All insurance policies required to be provided by Contractor under the Contract Documents shall name Owner, Program Manager and Architect
  as additional insureds.</TD></TR>
  <TR><TD align="left" width="6%" valign="top"><b>6.2</TD><TD width="99%"><b>Required Bonds. </b> Contractor, as part of the Work and the Contract Price, shall provide payment and performance bonds as follows: N/A</TD></TR></TABLE>
<br>
<b>VII. ENUMERATION OF CONTRACT DOCUMENTS</b>
<TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
  <TR><TD align="left" width="6%" valign="top"><b></TD><TD width="99%">The Contract Documents are made a part of this Prime Contract and incorporated herein by this reference. The Contract Documents, except for Modifications issued after
  execution of this Prime Contract, are this Prime Contract, the General Conditions, and the following documents:</TD></TR></TABLE>
</b><br>$(hold_enum)
<br><br><hr><br>
<div align="center"><font size="3">
<b>EXHIBIT "A"</b><br></div><br>
Inclusive of but not limited to:<br>
@getexhibit(job,group,seq,ckey,phase)
%IF ($(RETURN_CODE) == "100")
    <b>NO EXHIBIT ITEMS POSTED</b>
%ENDIF
<p><p>
<b>** A list of all persons and/or entities selected to perform as subcontractors to this contract shall be submitted
for Owner's review and approval prior to execution of subcontracts in accordance with the General Conditions of the
Specifications, Section IV, Paragraph 401.<p><p>
<B>ALL WORK TO COMPLY WITH ALL APPLICABLE CODES AND INDUSTRY STANDARDS.</b><p><br></b>
THIS PRIME CONTRACT is entered into as of the day and year first written above and is executed in at least two copies of which one is to be delivered to the Contractor and one to the Owner.
<br><br><b>This contract can be executed in counterparts.<br><br>
%IF ($(status) == "S" || $(agentuser) > "" && $(ckey) != "KY40217120")
    @DTW_ASSIGN(today,$(agentsign))
    @DTW_SUBSTR($(today),"1","4",yr)
    @DTW_SUBSTR($(today),"6","2",mo)
    @DTW_SUBSTR($(today),"9","2",da)
    @DTW_CONCAT($(mo),"/",mm)
    @DTW_CONCAT($(da),"/",dd)
    @DTW_CONCAT($(mm),$(dd),mmdd)
    @DTW_CONCAT($(mmdd),$(yr),mmddyyyy)
    <TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
     <TR><TD align="left" width="50%">Date__________<b>$(mmddyyyy)</b>_____________________</TD><TD width="50%">Date______________________________________</TD></TR></TABLE><br>
   <TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
     %IF ($(hold_po_sign_web) == "S" || $(hold_po_sign_web) == "X")
       <TR><TD align="left" width="5%">By</TD><TD align="left" width="45%"><img src="/SIGN/$(agentuser).gif"></TD><TD width="50%">By_______________________________________</td></tr></TABLE><br>
     %ELSE
       <TR><TD align="left" width="5%">By</TD><TD align="left" width="45%"><img src="/SIGN/$(agentuser).gif"><font size="1"><b>OWNER/AGENT</b></TD><TD width="50%">By_______________________________________</td></tr></TABLE><br>
     %ENDIF
   @getsec(agentuser)
   <TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
     <TR><TD align="left" width="50%">Title______<b>$(position)</B>________</TD><TD width="50%">Title______________________________________</TD></TR></TABLE><br>
%ELIF (status == "T" || $(agentuser) > "" && $(ckey) == "KY40217120")
    @DTW_ASSIGN(today,$(agentsign))
    @DTW_SUBSTR($(today),"1","4",yr)
    @DTW_SUBSTR($(today),"6","2",mo)
    @DTW_SUBSTR($(today),"9","2",da)
    @DTW_CONCAT($(mo),"/",mm)
    @DTW_CONCAT($(da),"/",dd)
    @DTW_CONCAT($(mm),$(dd),mmdd)
    @DTW_CONCAT($(mmdd),$(yr),mmddyyyy)
   <TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
     <TR><TD align="left" width="50%">Date______________________________________</TD><TD width="50%">Date__________<b>$(mmddyyyy)</b>__________________</TD></TR></TABLE><br>
   <TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
     <TR><TD align="left" width="50%">By________________________________________</TD><TD align="left" width="5%">By</TD><TD align="left" width="45%"><img src="/SIGN/$(agentuser).gif"></td></tr></TABLE><br>
   @getsec(agentuser)
   <TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
     <TR><TD align="left" width="50%">Title_______________________________________</TD><TD width="50%">Title______<b>$(position)</B>________</TD></TR></TABLE><br>
%ELSE
   <TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
     <TR><TD align="left" width="50%">Date_______________________________________</TD><TD width="50%">Date______________________________________</TD></TR></TABLE><br>
   <TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
     <TR><TD align="left" width="50%">By________________________________________</TD><TD width="50%">By_______________________________________</TD></TR></TABLE><br>
   <TABLE border="0" width="100%" cellpadding="0" cellspacing="0">
     <TR><TD align="left" width="50%">Title_______________________________________</TD><TD width="50%">Title______________________________________</TD></TR></TABLE><br>
%ENDIF
<TABLE border="0" width="125%" cellpadding="0" cellspacing="0">
  <TR><TD width="3%"></TD><TD align="left" width="35%">"$(hold_job_client)"</TD><TD width="50%">"$(hold_contractor_name)"</TD></TR></TABLE>
<p><br><font size="-1">(Phase $(phase) - $(hold_phase_name))<font size="2">
%IF ($(contract) == "X")
  <TABLE width="100%"><TR>
   <TD width="33%"></TD>
   <TD width="33%" align="center"><font face="arial" color="black" size="2"><img src="/GRAPHICS/report2.gif" name="img1" border="0" onMouseover="change1();" onMouseout="change11();" onClick="esign();"></a>APPROVE OR DISAPPROVE</TD>
   <TD width="33%"></TD></TR>
  </TABLE>
%ENDIF
</BODY>
</HTML>
