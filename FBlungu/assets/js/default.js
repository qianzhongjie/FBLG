// JavaScript Document


/*
页面load
*/
$(function(){
	$('a').live('click',function(){
        if(typeof($(this).attr("ck"))!="undefined"){
            var afun = $(this).attr('ck');
            eval(afun+'(this)');
        }
    }).each(function(index, element) {
        if($(element).attr('href')=='#' || $(element).attr('href')==''){
			$(element).attr('href','javascript:;')
		}
    });

    
    $('.check_box').live('click',function(){
    	if($(this).hasClass('checkbox_select')){
    		$(this).removeClass('checkbox_select');
    	}else{
    		$(this).addClass('checkbox_select');
    	}
    });

    $('.tab-nav-grounp .tab-nav-item').live('click',function(){
		$(this).addClass('active').siblings().removeClass('active');
		$($(this).parents('.tab-nav-grounp').attr('for')).find('>div').hide();
		$($(this).attr('for')).show();
	});

	$(document.body).delegate('.dropdown .dropdown-menu a','click',function(){
	    $(this).parents('.dropdown').find('.btn').html($(this).text()+'<span class="caret"></span>');
	    $(this).parents('.dropdown').attr('data',$(this).attr('data'));
	})
	
    
    //添加ajax全局事件
    $(document).ajaxStart(ajaxOnStart).ajaxComplete(ajaxOnComplete).ajaxSuccess(ajaxOnSuccess).ajaxStop(ajaxOnStop);;
	function ajaxOnStart(event) {
		//$.messager.loading('正在加载数据，请稍等...');
	}
	function ajaxOnSuccess(event, xhr, settings) {
		//
	}
	function ajaxOnComplete(event, xhr, settings) {
		/*$.messager.loading('close');
		if(xhr.status != 200){
			$.messager.tips('加载失败','error');
		}*/
	}
	function ajaxOnStop(){
		
	}

	
	

})


//页面事件
function contactsEditUser(){
	$('.dialog-contactsadduser').dialog({
		title:'修改成员'
	});
	$('.dialog-contactsadduser').find('input[name="pwd"]').parent().hide();
	$('.dialog-contactsadduser').dialog('open');
}
function contactsEditPwd(){
	$('.dialog-contactseditpwd').dialog('open');
}

function left_right_layout(){
	$('.main-p-body,.contact-left,.contact-right').height($('.framework').height()-$('.main-p-heading').outerHeight());
	$('.contact-right').width($('.contact-body').width()-$('.contact-left').outerWidth());

}

function page(){
	var navObj = '<nav><ul class="pagination"><li><a href="javascript:;">1</a></li><li><a href="javascript:;">2</a></li><li class="active"><a href="javascript:;">3</a></li><li><a href="javascript:;">4</a></li><li><a href="javascript:;">5</a></li><li><a href="javascript:;" aria-label="Next"><span aria-hidden="true">&raquo;</span></a></li></ul></nav>';
}


