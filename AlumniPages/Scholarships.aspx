<%@ Page Title="" Language="C#" MasterPageFile="~/AlumniPages/AlumniHeadFoot.master" AutoEventWireup="true" CodeFile="Scholarships.aspx.cs" Inherits="AlumniPages_Scholarships" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<link id="Link1" href="../css/tabs.css" media="all" runat="server" rel="Stylesheet" type="text/css" />
<script type="text/javascript" src="../Scripts/modernizr.custom.04022.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <section id="content">
		<div class="top">
			<div class="container">
            <div class="clearfix">
				<h2 style="font-size:25px;text-align:right"><img src="../images/logobig3.png" />&nbsp;Student Scholarships</h2>
              <img src="../images/active.png" height="3px" width="100%" />      
             <br /><br />
              <center> <img alt="" src="../images/giving.jpg" style="height: 108px; width:950px;"/></center>
              
            <div id="tab-body">
            <div class="tab-container">
						<section class="tabs">
	            <input id="tab-1" type="radio" name="radio-set" class="tab-selector-1" checked="checked" />
		        <label for="tab-1" class="tab-label-1">Student's</label>
		
	            <input id="tab-2" type="radio" name="radio-set" class="tab-selector-2" />
		        <label for="tab-2" class="tab-label-2">Donate</label>
		
	            <input id="tab-3" type="radio" name="radio-set" class="tab-selector-3" />
		        <label for="tab-3" class="tab-label-3">Contributors</label>
			
	            <input id="tab-4" type="radio" name="radio-set" class="tab-selector-4" />
		        <label for="tab-4" class="tab-label-4">Aided Student's</label>
            
			    <div class="clear-shadow"></div>
			
		        <div class="tab-content">
			        <div class="content-1">
						<h2>About us</h2>
                        <p>The unique feature of SSN Institutions is the scholarships awarded to students to encourage merit and to make education accessible to students of all economic strata. The Institution has initiated a thriving tradition of over 400 scholarships offered every year to meritorious and deserving students.
                        A brief description of the various scholarships is given below and the amount in brackets is the approximate value of the scholarship.
                        </p>
                        <br />
						<h3>Merit Scholarship [Gift Code : MS]</h3>
						<p>
                        Based on the academic performance at the qualifying examinations for freshers, 
                        and in the case of senior students, the performance during the previous year. 
                        The scholarship offers a waiver of tuition fees, free hostel stay and boarding/grant
                        towards living expenses and an allowance for books. All the Engineering, MBA and MCA 
                        programs at the Institution are covered under the Scholarship program.
                        </p>
                        <br />
                        <h3>Merit-cum-Means Scholarship [Gift Code : MMS]</h3>
                        <p>
                        The scholarship offers a waiver of tuition fees, free hostel stay and boarding/grant 
                        towards living expenses and an allowance for books. Academic performance and demonstrated 
                        economic need are the key criteria for this scholarship.
                        </p>
                        <br />
                        <h3> Waiver of Tuition Fees [Gift Code : WTF]</h3>
                        <p>
                        First year students of the college who secure the highest marks in the Plus Two 
                        examinations have their entire tuition fees waived for the first academic year.
                        Similarly, senior students are offered scholarships on the basis of their previous 
                        year’s performance.
                        </p>
                        <br />
                        <h3>Walk-in-Walk-out Scholarships [Gift Code : WIWO]</h3>
                        <p>
                        A top ten rank holder of any State or Central Board Examination at the Plus Two or 
                        equivalent level are entitled to pursue the B.E. / B.Tech. degree at SSN College of
                        Engineering absolutely free of cost.
                        </p>
                        <br />
                        <h3>Rural Student Scholarships [Gift Code : RSS]</h3>
                        <p>
                        In order to promote social equity and justice, the toppers from rural Government 
                        Higher Secondary Schools are given direct admission UG Programs with full scholarship 
                        for the whole duration of the course.
                        </p>
                        <br />
                        <h3>Post Graduate Scholarships [Gift Code : PGS]</h3>
                        <p>
                        In order to promote research and encourage meritorious students to pursue post-graduate
                        studies, scholarships are awarded to 50% of the students admitted to the PG programs in engineering.
                        </p>
                        <br />
                        <h3>Sports Scholarship [Gift Code : SS]</h3>
                        <p>
                        Sports scholarships are awarded to students with an outstanding record in sports. 
                        Typically, students who have represented the State at National level competitions
                        conducted by accredited Sports Boards are eligible.
                        </p>
				    </div>
			        <div class="content-2">
						<h2>Fund Tranfers</h2>
                        <h3>For INR FUNDS (Except NRE Accounts)</h3>
						<p>
                        Remitter can send INR contributions from any account (Except NRE Account), and the bank 
                        will accept the funds without any additional documentation from the Remitter or from the
                        Trust
                        </p>
                        <br />
                        <p>
                        The remitter needs to provide only the following details
                          <br />
                          <table class="tabfund">
                            <tr><td>Bank Name : ICICI Bank Ltd</td></tr>
                            <tr><td>Branch : Nungambakkam , Chennai – 600034</td></tr>
                            <tr><td>Address : 110, PRAKASH PRESIDIUM, UTHAMAR GANDHI SALAI, (NUNGAMBAKKAM HIGH ROAD), CHENNAI. 600034</td></tr>
                            <tr><td>RTGS / NEFT Code : ICIC0000009</td></tr>
                            <tr><td>Account Name : SSN Trust</td></tr>
                            <tr><td>Account Number : 000901072623</td></tr>
                          </table>
                        </p><br />
                        <p>
                        In order to channelize contribution, the remitter should give the following details on the 
                        comment field of Electronic Fund transfer: [Name]/[Degree]/[Branch]/[Year of Passing]
                        </p><br />
                        <p>
                        After the transaction, the remitter should send a mail to alumniofficer@ssn.edu.in with 
                        Name of the remitter, Degree, Branch, Year of Passing, Amount transferred, Date of transfer,
                        Transaction Number along with the address to which the receipt is to be sent.
                        </p>
                        <br />
                        <h3>For INR FUNDS (From NRE Accounts)</h3>
						<p>
                        The remitter can transfer INR funds from NRE Account. The remitter needs to provide only 
                        the following details
                        <br />
                          <table class="tabfund">
                            <tr><td>Bank Name : ICICI Bank Ltd</td></tr>
                            <tr><td>Branch : Nungambakkam , Chennai – 600034</td></tr>
                            <tr><td>Address : 110, PRAKASH PRESIDIUM, UTHAMAR GANDHI SALAI, (NUNGAMBAKKAM HIGH ROAD), CHENNAI. 600034</td></tr>
                            <tr><td>RTGS / NEFT Code : ICIC0000009</td></tr>
                            <tr><td>Account Name : SSN Trust</td></tr>
                            <tr><td>Account Number : 000901072623</td></tr>
                          </table>
                        </p>
                        <br />
                        <p>
                        After the transaction is over the remitter should send a mail to the trust office at 
                        Trust.finance@ssn.edu.in with the following details (with a copy to Alumni Officer at 
                        alumniofficer@ssn.edu.in) 
                          <br />
                          <table class="tabfund">
                            <tr><td>Name of the Remitter</td></tr>
                            <tr><td>Remitter's Bank Name</td></tr>
                            <tr><td>Amount and Date of Transfer</td></tr>
                            <tr><td>Branch and year of passing of the remitter</td></tr>
                            <tr><td>Address to which the receipt is to be sent</td></tr>
                            <tr><td>Scanned copy of the Passport of the remitter (first two pages and the last two pages)</td></tr>
                          </table>
                        </p><br />
                        <p>
                        After the money is transferred, the bank will communicate to the Trust (through E-Mail) the details of such 
                        funds received and kept on hold for want of Passport copy
                        </p><br />
                        <p>
                        The mail will be sent to Trust.finance@ssn.edu.in.
                        </p>
                        <br />
                        <p>
                        The Trust has to provide send the scanned copy of the remitter’s Passport within 
                        3 working days of communication and only thereafter the funds will be released by the bank
                        into the trust accounts.
                        </p>
                        <br />
                        <h3>For Transfer of Foreign Funds</h3>
                        <p>
                         Only funds in Foreign currencies can be remitted in to the below mentioned account number
                        </p>
                        <br />
                        <p>
                        Details to be provided by the remitter to transfer the funds
                        </p>
                        <br />
                        <p>
                        The remitter can transfer INR funds from NRE Account.
                        The remitter needs to provide only the following details 
                        <table class="tabfund">
                            <tr><td>Bank Name : State bank of India </td></tr>
                            <tr><td>Branch : NO.5 FIRST CROSS STREET , KASTURBA NAGAR, ADYAR, CHENNAI 600 020 </td></tr>
                            <tr><td>Swift Code : SBININBB291 </td></tr>
                            <tr><td>BSR Code : 01115 </td></tr>
                            <tr><td>Account name : SSN Trust </td></tr>
                            <tr><td>Account number : 30128071688 </td></tr>
                        </table> 
                        </p>
                        <br />
                        <p>
                        No Other documentation will be required either from the Trust or the remitter for any 
                        foreign remittances received in the account. After the transaction the remitter should 
                        send a mail to Trust.finance@ssn.edu.in and alumniofficer@ssn.edu.in with details of 
                        Remitter’s name, Degree, Branch, Year of Passing, Amount transferred, Date of transfer,
                        Transaction Number and the address to which the receipt is to be sent
                        </p>
                        <br />
                        <p>
                        Write Cheque or Demand Draft in favor of “SSN Trust – ICICI Bank Account 000901072623” 
                        and send to the address
                        <br />
                        <table class="tabfund">
                        <tr><td>Administrative Officer, SSN Trust</td></tr>
                        <tr><td>211/95, V.M Street</td></tr>
                        <tr><td>Mylapore, Chennai – 600 004</td></tr>
                        <tr><td>Phone: 91 44 2498 2656, 2498 6474 </td></tr>
                        </table>
                        </p>
                        <br />
                        <p>
                         The alumni should send a mail to alumniofficer@ssn.edu.in with Name of the remitter, 
                         Degree, Branch, Year of Passing, Amount transferred, Date of transfer, Transaction Number
                         along with the address to which the receipt is to be sent
                        </p>
				    </div>
			        <div class="content-3" style="overflow:hidden;">
						<h2>Contributor List</h2>
                        <table class="bordered">
                            <thead>
                                <tr>
                                    <th>#</th>        
                                    <th>Dept. Name</th>
                                    <th>Batch</th>
                                    <th>Fund Raised (in Rs.)</th>
                                </tr>
                            </thead>
                                <tr>
                                    <td>1</td>        
                                    <td>B.E (Chem)</td>
                                    <td>2009</td>
                                    <td>2 Lakhs</td>
                                </tr>        
                                <tr>
                                    <td>2</td>        
                                    <td>MCA</td>
                                    <td>2009</td>
                                    <td>1.5 Lakhs</td>
                                </tr>
                                <tr>
                                    <td>3</td>        
                                    <td>B.E (EEE)</td>
                                    <td>2003</td>
                                    <td>1 Lakh</td>
                                </tr>    
                                <tr>
                                    <td>4</td>        
                                    <td>B.E (IT)</td>
                                    <td>2006</td>
                                    <td>50,000</td>
                                </tr>
                        </table>
				    </div>
				    <div class="content-4" style="overflow:hidden;">
						<h2>Contact</h2>
                        <p>You see? It's curious. Ted did figure it out - time travel. And when we get back, we gonna tell everyone. How it's possible, how it's done, what the dangers are. But then why fifty years in the future when the spacecraft encounters a black hole does the computer call it an 'unknown entry event'? Why don't they know? If they don't know, that means we never told anyone. And if we never told anyone it means we never made it back. Hence we die down here. Just as a matter of deductive logic.</p>
						<h3>Get in touch</h3>
						<p>Well, the way they make shows is, they make one show. That show's called a pilot. Then they show that show to the people who make shows, and on the strength of that one show they decide if they're going to make more shows. Some pilots get picked and become television programs. Some don't, become nothing. She starred in one of the ones that became nothing. </p>
				    </div>
		        </div>
			</section>

        </div>

		    </div>
        
            <br />
            </div>
         </div>
		</div>
	</section>


</asp:Content>

