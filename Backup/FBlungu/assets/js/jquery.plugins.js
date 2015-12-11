/*
jquery plugins
*/

//messager
$.messager = {
	close:function(obj){
		$(obj).fadeOut("normal",function(){
		   $(obj).remove();
		});
	},
	tips:function(title,scene,time){
		time = time==null ? 3000 : parseInt(time);
		var sceneclass = "";
		switch(scene){
			case 'error':
				sceneclass = "msg-tips-error";
				break;
		}
		var tipsobj = $('<div class="msg-tips '+ sceneclass +'">'+title+'</div>').appendTo($(document.body));
		$(tipsobj).hide();
		$(tipsobj).fadeIn();
		$(tipsobj).css('margin-left','-'+$(tipsobj).width()/2+'px');

		setTimeout(function(){
			$.messager.close($(tipsobj));
		},time)
		
	},

	//确认对话框         
	confirm:function(title,msg,icon,fn) {    
		var confirmobj = $('<div class="msg-confirm"><i class="icon-warn"></i>'+msg+'</div>').appendTo($(document.body));
		$(confirmobj).dialog({
			title:title,
			width:440,
			height:220,
			animate:true,
			closed:false,
			buttons:[{
				text:'确定',
				btnCls:'dialogbtn-primary',
				handler:function(){
					fn.call(this,true);
					$(confirmobj).dialog('destroy');
				}
			},{
				text:'取消',
				handler:function(){
					fn.call(this,false);
					$(confirmobj).dialog('destroy');
				}
			}],
			onClose: function (){
				$(confirmobj).dialog('destroy');
			}
		})
	},
	//加载框
	loading:function(options){
		if(options && options=='close'){
			$.messager.close($('.msg-tips-loading'));
			return;
		}
		if(!options || options==""){
			options ="正在加载..."
		}
		var tipsobj = $('<div class="msg-tips msg-tips-loading">'+options+'</div>').appendTo($(document.body));
		$(tipsobj).hide();
		$(tipsobj).fadeIn();
		$(tipsobj).css('margin-left','-'+$(tipsobj).width()/2+'px');
		
	}
	
};

//panel
(function($){
	// 渲染组件
    function createPanel(target) {
    	/*
		var options = $.data(target, "panel").options;
		if(options.modal){
			var maskobj = document.createElement("div");
			maskobj = $(maskobj).appendTo($(document.body));
			$(maskobj).addClass('panel-mask');
		}
		//var panelobj = document.createElement("div");
		var panelobj = $('<div></div>').appendTo($(document.body));
		$(panelobj).addClass('panel-content');
		
		target = $(target).appendTo(panelobj);
		
		return { mask: maskobj,panel: panelobj};
		*/
		var options = $.data(target, "panel").options;
		if(options.modal){
			var maskobj = document.createElement("div");
			maskobj = $(maskobj).appendTo($(document.body));
			$(target).before($(maskobj));
			$(maskobj).addClass('panel-mask');
		}
		panelobj = $(target).wrapAll("<div></div>").parent();
		$(panelobj).addClass('panel-content');

		return { mask: maskobj,panel: panelobj};
    };
	
	function init(jq){
		var options = $.data(jq, "panel").options;
		var panel = $.data(jq, "panel").panel;
		//options.width = $(panel).outerWidth();
		//options.height = $(panel).outerHeight();
		
		_resize(jq);
		$(window).resize(function(){
			$('.panel-content').each(function(index, element) {
                $(element).css({
					left:($(window).width()-$(element).outerWidth())/2+'px',
					top:($(window).height()-$(element).outerHeight())/2+'px'
				})
            });
			
		});

		if(options.closed){
			_close(jq,false);
		}
	}
	
	function _resize(jq,param){

		var options = $.data(jq, "panel").options;
		
		var panel = $.data(jq, "panel").panel;
		param = param == null ? options : param;
		
		$(panel).outerWidth(param.width);
		$(panel).outerHeight(param.height);
		
		$(panel).css({
			left:($(window).width()-$(panel).outerWidth())/2+'px',
			top:($(window).height()-$(panel).outerHeight())/2+'px'
		})
		
		$.data(jq, "panel").options.onResize();
	}
	
	function _close(jq,isEvent){
		var panel = $.data(jq, "panel");
		$(panel.mask).hide();
		$(panel.panel).hide();
		isEvent ? $.data(jq, "panel").options.onClose() : '';
		return jq;
	}
	//打开
	function _open(jq){
		var panel = $.data(jq, "panel");
		$(panel.mask).show();
		$(panel.panel).show();
		$.data(jq, "panel").options.onOpen();
		return jq;
	}
	//销毁
	function _destroy(jq){
		var panel = $.data(jq, "panel");
		$.data(jq, "panel").options.onDestroy();
		$.removeData(jq);
		$(panel.mask).remove();
		$(panel.panel).remove();
		
	}
	
	//实例化
    $.fn.panel = function (target, parm) {
        if (typeof target == "string") {
            return $.fn.panel.methods[target](this, parm);
        }
        target = target || {};
        return this.each(function () {
			
            var panel = $.data(this, "panel");
            if (panel) {
                $.extend(panel.options, target);
            } else {
				panel = $.data(this, "panel", { options: $.extend({},$.fn.panel.defaults,target)});
				var r = createPanel(this);
				panel = $.data(this, "panel", {
					options: panel.options,
					mask: r.mask,
					panel: r.panel
				});
				init(this);
            }
        });
    };
	//默认方法
    $.fn.panel.methods = {
		opts: function(jq){
			return $.data(jq[0], "panel");
		},
        //打开
        open: function (jq) {
            return _open(jq[0]);
        },
        //关闭
        close: function (jq) {
            return _close(jq[0],true);
        },
		destroy: function(jq){
			_destroy(jq[0],true);
		},
		resize: function(jq,param){
			return _resize(jq[0],param);
		}
    };

	//默认属性和事件 
    $.fn.panel.defaults = $.extend({},{
        modal: true,
		closed: true,//初始化的时候是否关闭组件
		bodyCls:'',//主体引入一个class
        //当打开过后触发
        onOpen: function () {},
		onClose: function () {},
		onDestroy: function() {},
		onResize: function(){}
    });
})(jQuery);

//dialog
(function($){
	// 渲染组件
    function createDialog(jq) {
		var dialog = $.data(jq, "dialog");
		var opts = dialog.options;
		$(jq).panel($.extend({}, opts, {
			closed:true,
			bodyCls:'dialog-body',
			onResize:function(){
				_resize(jq);
			}
		}));
		var panel = $(jq).panel('opts').panel;
		var _titleHtml = opts.titleHtml !== "" ? opts.titleHtml : '<p>'+opts.title+'</p>';
		var dialogheader = $('<div class="dialog-header '+opts.headCls+'">'+_titleHtml+'<div class="dialog-head-btn"></div></div>').appendTo(panel);
		$('.<a href="javascript:;"><i class="icon-close"></i></a>').appendTo($(dialogheader).find('.dialog-head-btn')).bind('click',function(){
			$(jq).dialog('close');
		});
		
		
		var dialogcontent = $('<div class="dialog-content"></div>').appendTo(panel);
		$(jq).appendTo(dialogcontent);
		
		if(opts.buttons.length>0){
			var dialogfooter = '<div class="dialog-footer"></div>';
			dialogfooter = $(dialogfooter).appendTo(panel);
			for(var i=0;i<opts.buttons.length;i++){
				var btncls = opts.buttons[i].btnCls==null ? 'dialogbtn-default' : opts.buttons[i].btnCls;
				var aobj = '<a href="javascript:;" class="dialogbtn '+ btncls +'">'+ opts.buttons[i].text +'</a>';
				$(aobj).appendTo($(dialogfooter)).bind('click',eval(opts.buttons[i].handler));
			}
		}
		
		
		$(jq).panel('resize',opts);
		_move(jq);
		
		//初始化是否关闭窗口
		if(opts.closed == false){
			_open(jq);
		}
		
    };
	
	function _resize(jq){
		var dialog = $.data(jq, "dialog");
		var opts = dialog.options;
		
		var panel = $(jq).panel('opts').panel;
		var _h = $(panel).height()-$(panel).find('.dialog-header').height();
		if(opts.buttons.length>0){
			_h = _h - $(panel).find('.dialog-footer').height();
		}
		
		$(panel).find('.dialog-content').height(_h+'px');

	}
	
	function _move(jq){
		var draggingopts = {
			isdragging: false
		}
		$.data(jq, "dialog-dragging",draggingopts);
		var panel = $(jq).panel('opts').panel;
		
		var iWidth = document.documentElement.clientWidth;
		var iHeight = document.documentElement.clientHeight;
		
		var moveX,moveY,moveTop,moveLeft;
		var dialogheaderobj = $(panel).find('.dialog-header');
		$(dialogheaderobj).bind('mousedown',function(e){
			moveX = e.clientX;
			moveY = e.clientY;
			moveTop = parseInt($(panel).css('top'));
			moveLeft = parseInt($(panel).css('left'));
			var draggingopts = {
				isdragging: true,
				moveX : moveX,
				moveY:moveY,
				moveTop:moveTop,
				moveLeft:moveLeft,
			}
			$.data(jq, "dialog-dragging",draggingopts);
			$.data(document.body,'nowdialog',jq);
			return false;
			
		})
		document.onmousemove = function(e) {
			var jq = $.data(document.body,'nowdialog');
			if(jq){
				var draggingopts = $.data(jq, "dialog-dragging");
				if($(jq).panel('opts') == undefined ){
					return;
				}
				var panel = $(jq).panel('opts').panel;
				if (draggingopts.isdragging) {
					var e = e || window.event;	
					var x = draggingopts.moveLeft + e.clientX - draggingopts.moveX;
					var y = draggingopts.moveTop + e.clientY - draggingopts.moveY;
					
					x = x<0 ? 0 : x;
					y = y<0 ? 0 : y;
					if(x>=0 && y>=0){
						$(panel).css({left:x+"px",top:y+"px"});
					}
					return false;
				}
			}else{
				return;
			}
			
		};
		$(document).mouseup(function(e) {
			var jq = $.data(document.body,'nowdialog');
			if(jq){
				var draggingopts = {
					isdragging: false
				}
				$.data(jq, "dialog-dragging",draggingopts);
			}
		})
	}
	
	
	function _close(jq,isEvent){
		var panel = $.data(jq, "panel");
		var opts = panel.options;
		if(opts.animate){
			$(panel.panel).removeClass('animationIn').addClass('animationOut');
			setTimeout(function(){
				$(panel.mask).hide();
				$(panel.panel).hide();
			},500)
		}else{
			$(panel.mask).hide();
			$(panel.panel).hide();
		}
		
		isEvent ? $.data(jq, "panel").options.onClose() : '';
		return jq;
	}
	//打开
	function _open(jq){
		var panel = $.data(jq, "panel");
		var opts = panel.options;
		$(panel.panel).find('.dialog-header p').html($.data(jq, "dialog").options.title);
		$(jq).panel('resize');
		$(jq).panel('open');
		if(opts.animate){
			$(panel.panel).removeClass('animationOut').addClass('animationIn');
		}
		$.data(document.body,'nowdialog',jq);
		return jq;
	}
	//销毁
	function _destroy(jq){
		var panel = $.data(jq, "panel");
		$.data(jq, "panel").options.onDestroy();
		
		var opts = panel.options;
		if(opts.animate){
			$(panel.panel).removeClass('animationIn').addClass('animationOut');
			setTimeout(function(){
				$(panel.mask).remove();
				$(panel.panel).remove();
			},500)
		}else{
			$(panel.mask).remove();
			$(panel.panel).remove();
		}
		
		
	}
	
	//实例化
    $.fn.dialog = function (target, parm) {
        if (typeof target == 'string'){
			var method = $.fn.dialog.methods[target];
			if (method){
				return method(this, parm);
			} else {
				return this.panel(target, parm);
			}
		}
		
        target = target || {};
        return this.each(function () {
            var dialog = $.data(this, "dialog");
            if (dialog) {
                $.extend(dialog.options, target);

            } else {
				dialog = $.data(this, 'dialog', {
					options: $.extend({}, $.fn.dialog.defaults, target)
				})
				createDialog(this);
            }
			
			
        });
    };
	//默认方法
    $.fn.dialog.methods = {
        //打开
        open: function (jq) {
            return _open(jq[0]);
        },
        //关闭
        close: function (jq) {
            return _close(jq[0],true);
        },
		destroy: function(jq){
			_destroy(jq[0],true);
		}
    };

	//默认属性和事件 
    $.fn.dialog.defaults = $.extend({}, $.fn.panel.defaults, {
		title: '',
		titleHtml:'',
		headCls:'',
		width:500,
		height:300,
		animate:false,
		closed:true,
		buttons:[],
        //当打开过后触发
        onddOpen: function () {},
		onClose: function () {},
		onDestroy: function() {}
    });
})(jQuery);


/*
下拉tree
*/
(function($){
	//初始化
	function init(jq){
		var selecttree = $.data(jq, "selecttree");
		var opts = selecttree.options;
		var hiddentext = '<input class="tree-input-selected" type="hidden" />';
		var ulobj = '<ul class="tree-input-list"><li class="input-list-input"><input type="text" /></li></ul>';
		var treeobj = '<div class="tree-input-tree"></div>';
		ulobj = $(ulobj).appendTo($(jq));
		treeobj = $(treeobj).appendTo($(jq));

		_initTree(jq,treeobj);

		$(ulobj).bind('click',function(event){
		    $(this).find('.input-list-input input').focus();
		    var selecttree = $.data($(this).parent()[0], "selecttree");
		    $(selecttree.treeobj).css('top',$(selecttree.listobj).position().top+$(selecttree.listobj).height()+6).css('width',$(selecttree.listobj).outerWidth())
		    $(selecttree.treeobj).show();
		    event.stopPropagation();
		})

		$(document).bind('click',function(){
		    $(treeobj).hide();
		})
		return {listobj:ulobj , treeobj:treeobj}
	}

	function _initTree(jq,treeobj){
		var selecttree = $.data(jq, "selecttree");
		var opts = selecttree.options;
		$(treeobj).jstree({
			'plugins':["wholerow"],
			'core' : {
		      'data':opts.treeData,
		      "check_callback" : true
		    }
		}).on('changed.jstree', function (e, data) {
		    treeChanged(jq,data);
		}).click(function(event) {
		    event.stopPropagation();
		}).on('load_node.jstree', function (e, data) {
	        $(treeobj).jstree('close_all');
	        $(treeobj).jstree('deselect_all');
	    }).on('close_all.jstree', function (e, data) {
	        $(treeobj).jstree('open_node',$(treeobj).jstree('get_json')[0]);
	        
	    });

		
	}

	//tree change
	function treeChanged(jq,data){
		var selecttree = $.data(jq, "selecttree");


		if(!data || !data.node){return;}
		var node = data.node;
	    var selectedarr = selecttree.selected;
	    if(selectedarr){
	      if($.inArray(node.id,selectedarr)>=0){
	        return;
	      }else{
	        selectedarr.push(node.id);
	      }
	    }else{
	      selectedarr = new Array();
	      selectedarr.push(node.id);
	    }
	    selecttree.selected = selectedarr;

	    var liobj = '<li class="input-list-item"><span class="item-content-icon"><i class="glyphicon glyphicon-folder-close"></i></span><span class="item-content-text">'+data.node.text+'</span><span class="item-content-close"><i class="glyphicon glyphicon-remove"></i></span></li>';
	    liobj = $(liobj).attr('treeid',node.id);
	    $(liobj).insertBefore($(selecttree.listobj).find('.input-list-input')).find('.item-content-close').bind('click',function(event){
	      var nodeid= $(this).parent().attr('treeid');
	      $(this).parent().remove();
	      selectedarr.splice($.inArray(nodeid,selectedarr),1)
	      selecttree.selected = selectedarr;
	      $(selecttree.treeobj).css('top',$(selecttree.listobj).position().top+$(selecttree.listobj).height()+6)
	      event.stopPropagation();
	    });    
	    $(selecttree.treeobj).css('top',$(selecttree.listobj).position().top+$(selecttree.listobj).height()+6)
	}

	//add default data
	function addSelected(jq,data){
		var selecttree = $.data(jq, "selecttree");
		selecttree.selected = new Array();
	    $(selecttree.listobj).find('.input-list-item').remove();
		_addselect(jq,data);
	}

	function _addselect(jq,data){
		for(var i=0;i<data.length;i++){
			var nodedata = {
				node:data[i]
			}
			treeChanged(jq,nodedata);
		}
	}

	function _destroy(jq){
		var selecttree = $.data(jq, "selecttree");
        $(selecttree.treeobj).jstree('destroy');
	}


	$.fn.selecttree = function (target, parm) {
        if (typeof target == 'string'){
			var method = $.fn.selecttree.methods[target];
			if (method){
				return method(this, parm);
			}
		}
		
        target = target || {};
        return this.each(function () {
            var selecttree = $.data(this, "selecttree");
            if (selecttree) {
                $.extend(selecttree.options, target);
            } else {
				selecttree = $.data(this, 'selecttree', {
					options: $.extend({}, $.fn.selecttree.defaults, target),
				})
				var o = init(this);
				selecttree = $.data(this, "selecttree", {
					options: selecttree.options,
					listobj: o.listobj,
					treeobj: o.treeobj,
					selected: ''
				});
            }
			
			addSelected(this,selecttree.options.selectData);
			
        });
    };

    //默认方法
    $.fn.selecttree.methods = {
        //设置选中数据
        setSelect: function (jq,data) {
            return addSelected(jq[0],data);
        },
        addSelect: function (jq,data) {
            return _addselect(jq[0],data);
        },
        getSelected: function(jq){
        	return $.data(jq[0], "selecttree").selected;
        },
        getTreeObj: function(jq){
        	return $.data(jq[0], "selecttree").treeobj;
        },
        destroyTree: function(jq){
        	_destroy(jq[0]);
        	return jq;
        },
        initTree: function(jq,data){
        	if(data){
        		$.data(jq[0], "selecttree").options.treeData = data;
        	}
        	_initTree(jq[0],$.data(jq[0], "selecttree").treeobj);
        },
        setTreeData: function(jq,data){
        	_destroy(jq[0]);
        	$(jq[0]).selecttree('initTree',data);
        }
    };

    $.fn.selecttree.defaults = $.extend({},{
        treeData: {},
        selectData: {}
    });

})(jQuery);


//自定义搜索
(function($){
	function init(jq){
		var selectedulobj = '<ul class="selected-list"><li class="input-list-input"><input type="text" size="4"></li></ul>';
		selectedulobj = $(selectedulobj).appendTo($(jq));

		var selectulobj = '<ul class="select-list"></ul>';
		selectulobj = $(selectulobj).appendTo($(jq));
		$(selectulobj).outerWidth($(selectedulobj).outerWidth());
		return {selectedobj:selectedulobj,selectobj:selectulobj}
	}

	function addItem(list,inputload){
		var opts = inputload.options;
		var selectedobj = inputload.selectedobj;
		var selectobj = inputload.selectobj;
		var selectedarr = inputload.selectedarr;
		var liobj = "";
		if(list.type == "0"){
			liobj = '<li class="select-list-item" itemid="'+list.id+'">'+list.text+'</li>';
		}else{
			liobj = '<li class="select-list-item" itemid="'+list.id+'">'+list.text+'</li>';
		}
		$(liobj).appendTo($(selectobj)).hover(function() {
	        $(selectobj).find('.active').removeClass('active');
	        $(this).addClass('active');
	    }, function() {}).bind('click',function(event){
	      	var o = $(this);
	      	var selectedli = '';
	      	
	      	var jsondata = {
	      		id:$(o).attr('itemid'),
	      		text:$(o).text()
	      	}
	      	addSelected(jsondata,inputload);
		    $(selectedobj).find('input').val('');
		    $('.select-list').hide();

		    event.stopPropagation();
	    });
	}
	function addSelected(jsondata,inputload){
		var opts = inputload.options;
		var selectedobj = inputload.selectedobj;
		var selectobj = inputload.selectobj;
		var selectedarr = inputload.selectedarr;
		if($.inArray(jsondata.itemid,selectedarr)>=0){
	        return;
	    }
	    if(!opts.isMultiple){
      		$(selectedobj).find('.selected-list-item').remove();
      		selectedarr = [];
      	}
	    var closeobj = '<span class="item-content-close"><i class="icon-close"></i></span>';
	    if(!opts.isMultiple){
      		closeobj = '';
      	}
		var selectedli = '<li class="selected-list-item" itemid="'+jsondata.id+'"><span class="item-content-text">'+jsondata.text+'</span>'+closeobj+'</li>';
		$(selectedli).insertBefore($(selectedobj).find('.input-list-input')).find('.item-content-close')
		.bind('click',function(event){
	    	selectedarr.splice($.inArray($($(this).parent()).attr('itemid'),selectedarr),1)
	    	$(this).parent().remove();
	    	event.stopPropagation();
	    });
	    if($.inArray(jsondata.id,selectedarr)>=0){
	        return;
	    }else{
	        selectedarr.push(jsondata.id);
	        var selectedarrs = new Array();
	        inputload.selectedarrs.push(jsondata);
	    }
	    inputload.selectedarr = selectedarr;
	}

	function addEvent(jq){
		var inputload = $.data(jq, "inputload");
		var opts = inputload.options;
		var selectedobj = inputload.selectedobj;
		var selectobj = inputload.selectobj;
		var selectedarr = inputload.selectedarr;


		$(selectedobj).find('input').focus(function(event) {
		    $(selectobj).show();
		    $(selectobj).html('<li style="padding:5px;">'+opts.tipsText+'</li>');
		}).keyup(function(event) {
		    if(event.keyCode == 38){//上翻
		    	if($(selectobj).find('.active').prev().length>0){
		    		$(selectobj).find('.active').removeClass('active').prev().addClass('active');
		    	}
		      	return;
		    }else if(event.keyCode == 40){//下翻
		    	if($(selectobj).find('.active').next().length>0){
				    $(selectobj).find('.active').removeClass('active').next().addClass('active');
				}
			    return;
		    }else if(event.keyCode == 13){//回车
			    $(selectobj).find('.active').click();
			    return;
		    }else{//搜索
				$(selectobj).show();
		    	var _v = $(this).val();
		    	var iCount = _v.replace(/[^\u0000-\u00ff]/g,"aa");
		    	iCount = iCount.length+2;
		    	this.size=iCount>12?12:iCount;
				$(selectobj).html('');
				$.ajax({
					url: opts.url,
					type: 'POST',
					dataType: 'json',
					data: {value: _v},
					success:function(data){
						if(data && data.code=="0"){
							var list = data.data;
							for(var i=0;i<list.length;i++){
								if($.inArray(list[i].id,selectedarr)>=0){
							        continue;
							    }
							    addItem(list[i],inputload);
							}
							if($(selectobj).find('li').length>0){
								$($(selectobj).find('li')[0]).addClass('active');
							}else{
								$(selectobj).html('<li>无搜索结果</li>');
							}
						}
					},
					error:function(error){
						//console.log(error);
					}
				});
		      
		    }
		});
		//编辑框点击
		$(selectedobj).click(function(event) {
		    $(selectedobj).find('input').focus();
		    event.stopPropagation();
		});
		//文档点击
		$(document).click(function(event) {
		    $('.select-list').hide();
		});
	}

	function _getValue(jq){
		var inputload = $.data(jq, "inputload");
		return inputload.selectedarrs;
	}
	function _setValue(jq,data){
		var inputload = $.data(jq, "inputload");
		var selectedarr = inputload.selectedarr;
		for(var i=0;i<data.length;i++){
			var jsondata = {
	      		id:data[i].id,
	      		text:data[i].text
	      	}
	      	if($.inArray(data[i].id,selectedarr)>=0){
		        continue;
		    }
	      	addSelected(jsondata,inputload);
		}
	}
	function _delValue(jq,target){
		var inputload = $.data(jq, "inputload");
		var selectedobj = inputload.selectedobj;
		var selectedarr = inputload.selectedarr;
		selectedarr.splice($.inArray(target,selectedarr),1)
	    $(selectedobj).find('li[itemid='+target+']').remove();
	    var selectedarrs = inputload.selectedarrs;
	    for(var i=0;i<selectedarrs.length;i++){
	    	if(selectedarrs[i].id == target){
	    		selectedarrs.splice(i,1);
	    		break;
	    	}
	    }
	    inputload.selectedarrs = selectedarrs;
	}

	function _clearValue(jq){
		var inputload = $.data(jq, "inputload");
		inputload.selectedarr = [];
		inputload.selectedarrs = [];
	}

	//实例化
    $.fn.inputanyload = function (target, parm) {
        if (typeof target == 'string'){
			var method = $.fn.inputanyload.methods[target];
			if (method){
				return method(this, parm);
			}
		}
		
        target = target || {};
        return this.each(function () {
            var inputload = $.data(this, "inputload");
            if (inputload) {
                $.extend(inputload.options, target);
            } else {
				inputload = $.data(this, 'inputload', {
					options: $.extend({}, $.fn.inputanyload.defaults, target)
				})
				var obj = init(this);
				inputload = $.data(this, "inputload", {
					options: inputload.options,
					selectedobj: obj.selectedobj,
					selectobj: obj.selectobj,
					selectedarr: new Array(),
					selectedarrs: new Array()
				});
				addEvent(this);
            }
			


        });
    };

    $.fn.inputanyload.methods = {
    	getValue:function(jq){
    		return _getValue(jq[0]);
    	},
    	setValue:function(jq,target){
    		return _setValue(jq[0],target);
    	},
    	delValue:function(jq,target){
			return _delValue(jq[0],target);
    	},
    	clearValue:function(jq){
    		return _clearValue(jq[0]);
    	}
    }

	//默认属性和事件 
    $.fn.inputanyload.defaults = $.extend({}, {
		url:'',
		tipsText:'输入搜索条件',
		isMultiple:true,
    });
})(jQuery);


//获取焦点光标最后
$.fn.focusEnd = function(){
	$.fn.setCursorPosition = function(position){  
	    if(this.lengh == 0) return this;  
	    return $(this).setSelection(position, position);  
	}  
	  
	$.fn.setSelection = function(selectionStart, selectionEnd) {  
	    if(this.lengh == 0) return this;  
	    input = this[0];  
	  
	    if (input.createTextRange) {  
	        var range = input.createTextRange();  
	        range.collapse(true);  
	        range.moveEnd('character', selectionEnd);  
	        range.moveStart('character', selectionStart);  
	        range.select();  
	    } else if (input.setSelectionRange) {  
	        input.focus();  
	        input.setSelectionRange(selectionStart, selectionEnd);  
	    }  
	  
	    return this;  
	} 
    this.setCursorPosition(this.val().length);  
};


//tab
(function(){
	$.fn.tab = function (target, parm) {
        return this.each(function () {
        	var tab = $.data(this, 'tab', {
				options: $.extend({}, $.fn.tab.defaults, target)
			})

        	var tabobj = this;
            $(this).find('.nav-tabs > li > a').click(function(event) {

            	$(tabobj).find('.tab-content >div').hide();
            	$('#'+$(this).attr('for')).show();

            	$(this).parent().siblings().removeClass('active');
            	$(this).parent().addClass('active');

            	var tab = $.data(tabobj, "tab").options;
            	tab.change.call(this,this);
            });
            $(this).find('.nav-tabs li.active a').click();
			
        });
    };
    $.fn.tab.defaults = {
        change:function(){}
    };

})(jQuery);