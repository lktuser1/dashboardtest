<!--
//------------------------------------
//Common functions used on the site.
//last modified:  08/01/2006 by CDW
//------------------------------------

//Global variables, used on forms.
var place = getQueryVariable('place');
var url = getQueryVariable('url');

//Run common functions on page load.
function init() {	
	if (document.getElementById('page').className.match('form')) { //setting some options on page if a form.
		formUtils();
	}
}

function formUtils() {
	//default fallback function if page specific formUtils() does not exist. DO NO DELETE.
}

//Get location on forms to return user to previous location
function formsLocation(goURL,ifsearch) {
	var place = escape(document.title);
	var fromURL = window.location.toString();
	if (ifsearch == null ) {
		window.location = goURL+"?place="+place+"&url="+escape(fromURL);
	} else if (ifsearch == "yes") {
		window.location = goURL;
	}
}
//Write link on 'thanks' pages of forms.
function writeReturnLink(place,url) {
	if (place != null && url != null) {
		document.write("Return to: <a href='"+unescape(url)+"'>"+unescape(place)+"</a>.");
	}
}

//Form - select box go script
function getOption(form) {
	if (form.options[form.selectedIndex].value != "#") {
		location = form.options[form.selectedIndex].value;
	}	
}
//Check email for basic struture like an @ sign on newsletter signup
function chkEmail(form) {
	if (!form.EmailAddress.value.match('@')) {
		alert("Please provide a properly formatted e-mail address.");
		form.EmailAddress.focus();
		return false;
	} else {
		return true;
	}
}
//show error if exists for subscription process
function showSignupError() {
	var error = getQueryVariable('error');
	var rExp = /\+/gi;
	error = error.replace(rExp,'&nbsp;');
	document.write("<div id='validSummary'><strong class='accenttext'>Error Message:</strong> "+(error)+"</div>");
	document.getElementById('validSummary').style.display = "block";
}

//Form Validation - basic check to see of field empty or not checked.
function formValidation(form) {
	var fields = form.elements;
	var details = "<ul>";
	var boxli, radli = 0;
	for (var i = 0; i < fields.length; i++ ){
		if (fields[i].className.match("required")) {
			//chk text and textarea fields
			if ((fields[i].type == 'text' || fields[i].type == 'textarea') && !fields[i].value.length > 0) {
				details += "<li>"+fields[i].title+"</li>";
			//chk radio buttons
			} else if (fields[i].type == 'radio' ) {
				//do something
				var rname = fields[i].name;
				var radio = document.getElementsByName(rname);
				for (cnt = x = 0; x < radio.length; x++ ) {
					if (radio[x].checked) cnt++; 
				}
				if (cnt == 0 && (!details.match(fields[i].title))) { //check to see if li already in 'details'
					radli++;
					details += "<li>"+fields[i].title+"</li>";
				}
			} 
			//check checkboxes (single or groups)
			else if (fields[i].type == 'checkbox') { 
				var boxName = fields[i].name;
				var boxes = document.getElementsByName(boxName);
				for (cnt = x = 0; x < boxes.length; x++ ) {
					if (boxes[x].checked) cnt++; 
				}
				if (cnt == 0 && (!details.match(fields[i].title))) { //check to see if li already in 'details'
					boxli++;
					details += "<li>"+fields[i].title+"</li>";
				}				
			} 
			//select drop downs
			else if (fields[i].tagName == 'SELECT' && (fields[i].value == '#' || fields[i].value == '')) { 
				details += "<li>"+fields[i].title+"</li>";
			} else if ( fields[i].type == 'hidden' ) {
				//skip
			}
		}
	}
	if (details != "<ul>") {
		details += '</ul>';
		document.getElementById('validDetails').innerHTML = details;	
		document.getElementById('validSummary').style.display = 'block';
		document.location = "#validSummary";
		return false; 
	} else  {
		blur_buttons(form);
		return true;
	}
}


//blur all buttons on the form
function blur_buttons(form) {
  for (var i=0; i<form.elements.length; i++) {
    if (form.elements[i].type=='button' || form.elements[i].type=='submit' ||
        form.elements[i].type=='reset') {
      form.elements[i].disabled = true;
    }
  }
}

//Get query string from URL.
function getQueryVariable(variable) {
	var query = window.location.search.substring(1);
	var vars = query.split("&");
	for (var i=0;i<vars.length;i++) {
		var pair = vars[i].split("=");
		if (pair[0] == variable) {
			return pair[1];
		}
  	} 
}

//Highlight side nav category. Show/hide 2nd level nav.
function sidenav(topic,subtopic) {
	if ( topic != '' ) {
		document.getElementById(topic).className = 'topicon';
	}	
	if ( subtopic != '' ) {
  		document.getElementById(subtopic).className = 'subtopicson';
  	}
}

//Page Tool - Add page to favorites.
function pageToolsBookmark() {
	bkmkurl= document.URL;
	bkmktitle= document.title;
	//if (document.all) {
	if (window.external) {
		window.external.AddFavorite(bkmkurl,bkmktitle);
	} else {
		alert("Please use Control + D to set a bookmark for this page");
	}
}

//Page Tool - Email page to coworkers
function pageToolsEmail() {
	var title = document.title;
	var subj = escape(title);
	var bodyTxt = "Hi, I thought you might like to see this EDS intranet page: " + title + ", found at: " + document.URL + ". This link will take you to the EDS intranet.";
	bodyTxt = escape(bodyTxt);
	document.writeln("<a onMouseOver=\"window.status='Recommend this page to a coworker.'; return true\" onMouseOut=\"window.status=' ';return true\" onFocus=\"window.status='Recommend this Web page';return true;\" onBlur=\"window.status=' ';return true;\" href='mailto:\?subject=" + subj + "\&body\=" + bodyTxt + "' title=\"Recommend this page to a coworker.\" id=\"emailthis\">e-mail</a>");
}

//START format links for print
//Based on Aaron Gustafson Article from ALA: http://www.alistapart.com/articles/improvingprint
//Removed super script numbering code as inconsistent as it counted links we 'hide'
function footnoteLinks(containerID,targetID) {
	if (!document.getElementById || !document.getElementsByTagName || !document.createElement) return false;
  if (!document.getElementById(containerID) || !document.getElementById(targetID)) return false;
   var container = document.getElementById(containerID);
   var target = document.getElementById(targetID);
   var h2 = document.createElement('h2');
   addClass.apply(h2,['printOnly']);
   var h2_txt = document.createTextNode('Links on page:');
   h2.appendChild(h2_txt);
   var coll = container.getElementsByTagName('a');
   var ol = document.createElement('ol');
   addClass.apply(ol,['printOnly']);
   var myArr = [];
   var thisLink;
   for (var i=0; i<coll.length; i++) {
   	if ( coll[i].getAttribute('href') || coll[i].getAttribute('cite') ) { 
			thisLink = coll[i].getAttribute('href') ? coll[i].href : coll[i].cite;
			var note = document.createElement('sup');
			addClass.apply(note,['printOnly']);
			var note_txt;
			if (!thisLink.match('mailto') && !thisLink.match('peoplesearch') && !thisLink.match('#top') && (!thisLink.match(document.location) && !thisLink.match('#')) ) { //links to avoid
				var j = inArray.apply(myArr,[thisLink]);
				if ( j || j===0 ) { 
					//if a duplicate, skip it
				} else { // if not a duplicate
					var li = document.createElement('li');
					var li_txt = document.createTextNode(thisLink);
					li.appendChild(li_txt);
					ol.appendChild(li);
					myArr.push(thisLink);
				}
			}
         //code here for attaching super script numbering
   	} 
	}
	if (myArr != null) {
		target.appendChild(h2);
   	target.appendChild(ol);
	}
   return true;
}
//Call the print links code.
window.onload = function() {   
	footnoteLinks('main','footer');
} 

//Code needed for printing of links on page.
if(Array.prototype.push == null) {
	Array.prototype.push = function(item) {
		this[this.length] = item;
		return this.length;
   };
};
// ----------------------------------------------------
//           function.apply (if unsupported)
//    Courtesy of Aaron Boodman - http://youngpup.net
// ----------------------------------------------------
if (!Function.prototype.apply) {
	Function.prototype.apply = function(oScope, args) {
   	var sarg = [];
      var rtrn, call;
      if (!oScope) oScope = window;
      if (!args) args = [];
      for (var i = 0; i < args.length; i++) {
      	sarg[i] = "args["+i+"]";
      };
      call = "oScope.__applyTemp__(" + sarg.join(",") + ");";
      oScope.__applyTemp__ = this;
      rtrn = eval(call);
      oScope.__applyTemp__ = null;
    	return rtrn;
      };
};
function inArray(needle) {
	for (var i=0; i < this.length; i++) {
   	if (this[i] === needle) {
      	return i;
      }	
	}
   return false;
}
function addClass(theClass) {
	if (this.className != '') {
      this.className += ' ' + theClass;
   } else {
   	this.className = theClass;
   }
}
//Removed last function as not used.
//END format links for print

//convert to word script (created by P. Rail) 
var tout = 0;
function toggle_view( theID ) {
   if ( tout != 0 ) can_timeout();
   var area = document.getElementById(theID);
   area.style.display = 'block';
   var cmd = "document.getElementById('" + theID + "').style.display = 'none'";
   tout = setTimeout( cmd, 20000 );
}
 
function can_timeout() {
   clearTimeout(tout);
   tout = 0;
}

// Used to change the display language
function changeLanguage()
{
		var expirationDate = new Date()
		var theForm = document.forms['frmNavigator']
		var langIndex = theForm.ddlLanguage.selectedIndex
		var langID = theForm.ddlLanguage[langIndex].value
		theForm.language.value = langID;
		expirationDate.setYear(expirationDate.getYear()+5)
				
		setCookie("language",langID,expirationDate,"/")
		
		return(theForm.submit());
}

// Used to set a cookie on the user's computer
function setCookie (name,value,expires,path,domain,secure) {
  document.cookie = name + "=" + escape (value) +
    ((expires) ? "; expires=" + expires.toGMTString() : "") +
    ((path) ? "; path=" + path : "") +
    ((domain) ? "; domain=" + domain : "") +
    ((secure) ? "; secure" : "")
}

/*'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Name       :  fobjForm
' Purpose    :  Returns a reference to the specified HTML form.
' Parameters :  pstrFormID    - ID property of the HTML form.
' Return val :  Object
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''*/
function fobjForm(pstrFormID) {
	return document.forms[pstrFormID];
}

/*'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' Name       :  fobjFormControl
' Purpose    :  Returns reference to the specified control.
' Parameters :  pstrFormID    - ID property of the HTML form.
'               pstrControlID - ID property of the HTML control.
' Return val :  Object
''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''*/
function fobjFormControl(pstrFormID, pstrControlID) 
{
	if (pstrFormID.length > 0)
	{
		var objForm = fobjForm(pstrFormID);
		return objForm.elements[pstrControlID];
	}
	else
	{
		return document.getElementById(pstrControlID);	
	}	
}

//Function to navigate to a url
function NavToUrl(theUrl)
{ 
    document.location.href = theUrl; 
}
-->